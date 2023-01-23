namespace EventSystem.Base
{
    public abstract class EventItem
    {
        protected EventBase eventBase;

        protected enum EventItemType
        {
            action,
            condition
        }
        
        //returns if the items is an action or a condition
        protected abstract EventItemType OfType();
        
        
        //has to be called by the script to hand control over to the event System
        public void EndEventItem()
        {
            eventBase.EventItemRanCallback(this);
        }
        
        //gets called by the event system when the event item is played
        public abstract void OnItemStart();
        
        //Called by event system
        public void DispatchItem(EventBase eventBase)
        {
            this.eventBase = eventBase;
            this.OnItemStart();
        }
        
        //called by the event system to resume execution
        public abstract void OnResumeExecution();
        
    }
}