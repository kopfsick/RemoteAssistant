namespace RemoteAssistant.API.Power
{
    public class ShutDownPowerCommand : PowerCommandBase
    {
        public override string Command => "ShutDown";
        public override string Switch => "/s /t 10";
    }
}