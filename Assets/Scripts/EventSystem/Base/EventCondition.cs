namespace EventSystem.Base
{
    public abstract class EventCondition : EventItem
    {
        protected override EventItemType OfType()
        {
            return EventItemType.condition;
        }
    }
}