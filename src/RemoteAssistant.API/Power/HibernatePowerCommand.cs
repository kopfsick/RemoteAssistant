using System.Threading.Tasks;

namespace RemoteAssistant.API.Power
{
    public class HibernatePowerCommand : IPowerCommand
    {
        public string Command => "Hibernate";
        public Task<string> ExecuteAsync()
        {
            return Task.FromResult("You chose HIIIIBERNATE!");
        }
    }
}