namespace EventSystem
{
    public class SequentialEventTest : SequentialEvent
    {
        public ActionExecuteCondition playerInGarageCondition; 
        public ActionExecuteCondition playerInGardenCondition; 
        public override void InitEvent()
        {

            AddAction(new TestAction("Go to the garage"));
            AddAction(new TestAction("oh player is in garage :O").AddCondition(playerInGarageCondition));
            AddAction(new TestAction("Go to the garden"));
            AddAction(new TestAction("player is in the garden").AddCondition(playerInGardenCondition));
            AddAction(new TestAction("final action"));
            StartSequentialEvent();
        }
    }
}
