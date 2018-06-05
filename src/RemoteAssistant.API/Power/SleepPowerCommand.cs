using System.Threading.Tasks;

namespace RemoteAssistant.API.Power
{
    public class SleepPowerCommand : IPowerCommand
    {
        public string Command => "Sleep";
        public Task<string> ExecuteAsync()
        {
            return Task.FromResult("You chose SLEEEEP!");
        }
    }
}