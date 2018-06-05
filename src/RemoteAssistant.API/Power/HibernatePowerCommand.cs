namespace RemoteAssistant.API.Power
{
    public class HibernatePowerCommand : PowerCommandBase
    {
        public override string Command => "Hibernate";
        public override string Switch => "/h";
    }
}