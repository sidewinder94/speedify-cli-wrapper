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
        private static string[] _possibleCliPaths =
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

            p.WaitForExit((int)timeout * 1000);

            p.Close();

            return outputBuffer.ToString();
        }

        private void LongRunningSpeedifyCommand(Action<string> callBack, CancellationToken cancellationToken, int timeout = 0, params string[] args)
        {
            if (timeout < 0)
            {
                throw new ArgumentException("Timeout cannot be negative ", nameof(timeout));
            }

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

            p.OutputDataReceived += (sender, eventArgs) =>
            {
                outputBuffer.AppendLine(eventArgs.Data);
                if (eventArgs.Data == string.Empty)
                {
                    callBack(outputBuffer.ToString());
                    outputBuffer.Clear();
                }
            };

            p.Start();
            p.BeginOutputReadLine();


            if (timeout > 0)
            {
                p.WaitForExit((int)timeout * 1000);
                p.Close();
                return;
            }

            cancellationToken.Register(() =>
            {
                p.Kill();
                p.Close();
            });

            p.WaitForExit();
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

                foreach (var possiblePath in _possibleCliPaths)
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

        public SpeedifyStats Stats(int duration = 0)
        {
            var result = new SpeedifyStats();

            var cancel = new CancellationTokenSource();

            this.LongRunningSpeedifyCommand((s) => this.HandleCustomJson(s, result), cancel.Token, 0, "stats", "10");

            return result;
        }

    }
}