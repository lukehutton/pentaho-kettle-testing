using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Spike.Specs
{
    public class CommandExecutor
    {
        private const int MillisecondsTimeout = 60000;

        public static void Execute(string command)
        {
            int exitCode = -1;
            using (var process = new Process())
            {
                Console.WriteLine("command>>" + command);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + command;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                var output = new StringBuilder();
                var error = new StringBuilder();

                using (var outputWaitHandle = new AutoResetEvent(false))
                using (var errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                outputWaitHandle.Set();
                            }
                            else
                            {
                                output.AppendLine(e.Data);
                            }
                        };
                    process.ErrorDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                errorWaitHandle.Set();
                            }
                            else
                            {
                                error.AppendLine(e.Data);
                            }
                        };

                    process.Start();

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    
                    if (process.WaitForExit(MillisecondsTimeout) &&
                        outputWaitHandle.WaitOne(MillisecondsTimeout) &&
                        errorWaitHandle.WaitOne(MillisecondsTimeout))
                    {
                        exitCode = process.ExitCode;

                        Console.WriteLine("output>>");
                        Console.WriteLine(output.ToString());
                        if (error.Length > 0)
                        {
                            Console.WriteLine("error>>");
                            Console.WriteLine(error.ToString());
                        }
                        if (exitCode != 0)
                        {
                            throw new Exception(string.Format("command failed: ({0}) : {1}", exitCode, error));
                        }
                    }
                    else
                    {
                        Console.WriteLine("CommandExecutor.Execute timed out in {0} ms.", MillisecondsTimeout);
                    }
                }
            }

            Console.WriteLine("CommandExecutor.Execute ExitCode: " + exitCode.ToString());
        }
    }
}