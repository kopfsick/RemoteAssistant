using System.Threading.Tasks;

namespace RemoteAssistant.API.Power
{
    public interface IPowerCommand
    {
        string Command { get; }
        Task<string> ExecuteAsync();
    }
}
