using System;

namespace EventSystem.Base
{
    public class EventBasedCondition: EventCondition
    {
        private readonly TriggerCondition condition;
        public EventBasedCondition(TriggerCondition condition)
        {
            this.condition = condition;
        }

        public override void OnItemStart()
        {
            condition.Triggered += OnConditionTriggered;
        }

        public override void OnResumeExecution()
        {
            throw new System.NotImplementedException();
        }

        private void OnConditionTriggered(object sender, EventArgs args)
        {
            condition.Triggered -= OnConditionTriggered;
            EndEventItem();
        }
    }
}