using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Newtonsoft.Json;
using SpeedifyCliWrapper.ReturnTypes;

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

    }
}