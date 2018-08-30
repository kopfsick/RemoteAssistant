namespace RemoteAssistant.API.Power
{
    public class SleepPowerCommand : SuspendCommandBase
    {
        public override string Command => "Sleep";
        public override string Switch => "Sleep";
    }
}