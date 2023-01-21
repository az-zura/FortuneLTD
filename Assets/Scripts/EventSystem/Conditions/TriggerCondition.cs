using System;

namespace EventSystem.Base
{
    public class TriggerCondition
    {
        public event EventHandler Triggered;

        public void OnEventConditionMet()
        {
            EventHandler handler = Triggered;
            handler?.Invoke(this,EventArgs.Empty);
        }
    }
}