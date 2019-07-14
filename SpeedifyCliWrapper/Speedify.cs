using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpeedifyCliWrapper.Enums;
using SpeedifyCliWrapper.Modules;
using SpeedifyCliWrapper.ReturnTypes;
using Timer = System.Timers.Timer;

namespace SpeedifyCliWrapper
{
    public class Speedify
    {
        private static readonly string[] PossibleCliPaths =
        {
            @"/Applications/Speedify.app/Contents/Resources/speedify_cli",
            @"C:\Program Files (x86)\Speedify\speedify_cli.exe",
            @"C:\Program Files\Speedify\speedify_cli.exe",
            @"/usr/share/speedify/speedify_cli"
        };

        private string _cliPath;

        public string CliPath
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this._cliPath))
                {
                    return this._cliPath;
                }

                foreach (var possiblePath in PossibleCliPaths)
                {
                    if (File.Exists(possiblePath))
                    {
                        this._cliPath = possiblePath;
                        return this._cliPath;
                    }
                }

                throw new InvalidOperationException("Speedify CLI executable not found");
            }
        }

        public Show Show { get; private set; }

        public Speedify()
        {
            this.Show = new Show(this);
        }

        #region Run Command Methods

        internal T RunSpeedifyCommand<T>(int timeout = 60, params string[] args)
        {
            var value = this.RunSpeedifyCommand(timeout, args);

            return JsonConvert.DeserializeObject<T>(value);
        }

        private string RunSpeedifyCommand(int timeout = 60, params string[] args)
        {
            if (timeout < 0)
            {
                throw new ArgumentException("Timeout cannot be negative ", nameof(timeout));
            }

            var timeoutSpan = new TimeSpan(0, 0, timeout);

            var pi = new ProcessStartInfo(this.CliPath, string.Join(" ", args))
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            StringBuilder outputBuffer = new StringBuilder();

            var p = new Process()
            {
                StartInfo = pi

            };

            p.OutputDataReceived += (sender, eventArgs) => outputBuffer.AppendLine(eventArgs.Data);

            p.Start();
            p.BeginOutputReadLine();

            while (!p.HasExited && (DateTime.Now - p.StartTime < timeoutSpan))
            {
                p.WaitForExit(200);
            }

            if (!p.HasExited)
            {
                p.WaitForExit(1);
                p.Kill();
            }

            p.WaitForExit(1);
            p.Close();

            return outputBuffer.ToString();
        }

        private void LongRunningSpeedifyCommand(Action<string> callBack, CancellationToken cancellationToken, int timeout = 0, params string[] args)
        {
            if (timeout < 0)
            {
                throw new ArgumentException("Timeout cannot be negative ", nameof(timeout));
            }

            var timeoutSpan = new TimeSpan(0, 0, timeout);

            if (!cancellationToken.CanBeCanceled)
            {
                throw new ArgumentException("Cancellation Token must be cancellable ", nameof(cancellationToken));
            }

            var pi = new ProcessStartInfo(this.CliPath, string.Join(" ", args))
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            StringBuilder outputBuffer = new StringBuilder();

            var p = new Process()
            {
                StartInfo = pi
            };

            var sent = false;

            p.OutputDataReceived += (sender, eventArgs) =>
            {

                outputBuffer.AppendLine(eventArgs.Data);
                if (eventArgs.Data == string.Empty)
                {
                    if (!sent)
                    {
                        callBack(outputBuffer.ToString());
                        outputBuffer.Clear();
                        sent = true;
                    }
                }
                else
                {
                    sent = false;
                }
            };

            p.Start();
            p.BeginOutputReadLine();

            if (timeout > 0)
            {
                while (!p.HasExited && (DateTime.Now - p.StartTime < timeoutSpan))
                {
                    p.WaitForExit(200);
                }

                if (!p.HasExited)
                {
                    p.WaitForExit(1);
                    p.Kill();
                }

                p.WaitForExit(1);
                p.Close();
            }

            cancellationToken.Register(() =>
            {
                p.WaitForExit(1);
                p.Kill();
                p.Close();
            });


            while (!cancellationToken.IsCancellationRequested)
            {
                p.WaitForExit(200);
            }

        }

        private void HandleCustomJson<T>(string json, T objectToPopulate) where T : ICustomJson, new()
        {
            var jsonObject = JsonConvert.DeserializeObject(json);
            var jsonChildrens = ((JArray)jsonObject).Children();

            var objectName = jsonChildrens.First();

            if (objectName.Type != JTokenType.String)
            {
                throw new InvalidOperationException("invalid Json");
            }

            MethodInfo mi = objectToPopulate[objectName.Value<string>()];

            Type typeToStore = mi.GetParameters().Single().ParameterType;

            mi.Invoke(objectToPopulate, new[] { jsonChildrens.Skip(1).First().ToObject(typeToStore) });
        }

        #endregion

        /// <summary>
        /// Returns current speedify version
        /// </summary>
        /// <returns></returns>
        public SpeedifyVersion Version(int timeout = 60)
        {
            return this.RunSpeedifyCommand<SpeedifyVersion>(args: "version", timeout: timeout);
        }

        #region Stats related methods

        /// <summary>
        /// Returns a populated stats object
        /// </summary>
        /// <param name="duration">The duration to wait for the speedify CLI to gather the data in seconds</param>
        /// <param name="timeout">Returns after the given duration in seconds if the process didn't return </param>
        /// <returns>A stats object</returns>
        /// <remarks>A duration less than 3 will probably not return any result</remarks>
        public SpeedifyStats Stats(int duration = 3, int timeout = 60)
        {
            var result = new SpeedifyStats();

            //Time at 3 is the minimum for which we'll get something back
            var fusedJson = this.RunSpeedifyCommand(args: new[] { "stats", duration.ToString() }, timeout: timeout);

            var splittedJson = fusedJson.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var json in splittedJson.Where(sj => !string.IsNullOrWhiteSpace(sj)))
            {
                this.HandleCustomJson(json, result);
            }

            return result;
        }

        /// <summary>
        /// Refresh the current stats objects, by default returns after 1 sec
        /// </summary>
        /// <param name="toRefresh">The object to update</param>
        /// <param name="duration">The duration the command should run 0 is indefinite</param>
        /// <param name="timeout">The maximum timeout 0 is indefinite</param>
        /// <remarks>The timeout and duration are there to keep each other in check, the lowest one should make the method return</remarks>
        public void RefreshStats(SpeedifyStats toRefresh, int duration = 1, int timeout = 0)
        {
            var cancel = new CancellationTokenSource();

            if (duration > 0)
            {
                cancel.CancelAfter(new TimeSpan(0, 0, duration));
            }

            this.AsynRefreshStats(toRefresh, cancel.Token, timeout).Wait();
        }

        /// <summary>
        /// Refresh the current stats object asynchronously, by default will run until cancelled or the timeout is hit
        /// </summary>
        /// <param name="toRefresh">The object to be updated while the method runs</param>
        /// <param name="cancellationToken">The cancellation token used to check if the method should stop</param>
        /// <param name="timeout">If hit before the cancellation token is cancelled, the refresh will stop</param>
        /// <returns>The task that is running the refresh.</returns>
        public Task AsynRefreshStats(SpeedifyStats toRefresh, CancellationToken cancellationToken, int timeout = 0)
        {
            return Task.Run(() =>
            {
                this.LongRunningSpeedifyCommand(s => this.HandleCustomJson(s, toRefresh), cancellationToken,
                    timeout, "stats");
            });

        }

        #endregion

        public SpeedifySettings Mode(BondingMode mode = BondingMode.Speed, int timeout = 60)
        {
            return this.RunSpeedifyCommand<SpeedifySettings>(args: new[] { "mode", mode.ToString().ToLower() }, timeout: timeout);
        }
    }
}