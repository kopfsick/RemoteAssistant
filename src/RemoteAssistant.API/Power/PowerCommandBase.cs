using System.Diagnostics;
using System.Threading.Tasks;

namespace RemoteAssistant.API.Power
{
    public abstract class PowerCommandBase : IPowerCommand
    {
        public abstract string Command { get; }
        public abstract string Switch { get; }

        public Task<string> ExecuteAsync()
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "shutdown.exe",
                    Arguments= $"{Switch} /f",
                }
            };

            process.Start();

            return Task.FromResult(Command);
        }
    }
}