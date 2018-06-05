namespace RemoteAssistant.API.Power
{
    public class AbortShutDownPowerCommand : PowerCommandBase
    {
        public override string Command => "AbortShutDown";
        public override string Switch => "/a";
    }
}