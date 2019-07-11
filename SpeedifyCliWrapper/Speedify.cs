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

        private T RunSpeedifyCommand<T>(int timeout = 60, params string[] args)
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

            var timeoutSpan = new TimeSpan(0, 0, 60);

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

            var timeoutSpan = new TimeSpan(0, 0, 60);

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


        public void HandleCustomJson<T>(string json, T objectToPopulate) where T : ICustomJson, new()
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

        /// <summary>
        /// Returns current speedify version
        /// </summary>
        /// <returns></returns>
        public SpeedifyVersion Version()
        {
            return this.RunSpeedifyCommand<SpeedifyVersion>(args: "version");
        }

        public SpeedifyStats Stats(int duration = 3)
        {
            var result = new SpeedifyStats();

            //Time at 3 is the minimum for which we'll get something back
            var fusedJson = this.RunSpeedifyCommand(args: new[] { "stats", duration.ToString() });

            var splittedJson = fusedJson.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var json in splittedJson.Where(sj => !string.IsNullOrWhiteSpace(sj)))
            {
                this.HandleCustomJson(json, result);
            }

            return result;
        }

        public void RefreshStats(SpeedifyStats toRefresh, int duration = 1, int timeout = 0)
        {

            var cancel = new CancellationTokenSource();

            if (duration > 0)
            {
                cancel.CancelAfter(new TimeSpan(0, 0, duration));
            }

            this.AsynRefreshStats(toRefresh, cancel.Token, timeout).Wait();
        }

        public Task AsynRefreshStats(SpeedifyStats toRefresh, CancellationToken cancellationToken, int timeout = 0)
        {
            return Task.Run(() =>
            {
                this.LongRunningSpeedifyCommand(s => this.HandleCustomJson(s, toRefresh), cancellationToken,
                    timeout, "stats");
            });

        }
    }
}