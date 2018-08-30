using System.Threading.Tasks;
using System.Diagnostics;

namespace RemoteAssistant.API.Power
{
    public abstract class SuspendCommandBase : IPowerCommand
    {
        public abstract string Command { get; }
        public abstract string Switch { get; }

        public Task<string> ExecuteAsync()
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "Rundll32.exe",
                    Arguments= $"Powrprof.dll,SetSuspendState {Switch}",
                }
            };

            process.Start();

            return Task.FromResult(Command);
        }
    }
}