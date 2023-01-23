using System.Collections;
using System.Collections.Generic;

namespace EventSystem.Base
{
    public abstract class ActionBase : EventItem
    {
        protected override EventItemType OfType()
        {
            return EventItemType.action;
        }

        //calls event system to suspend execution
        public void SuspendAction(float timeInSeconds)
        {
            eventBase.SuspendEventItem(this,timeInSeconds);
        }

        public void DispatchCoroutine(IEnumerator coroutine)
        {
            this.eventBase.ExecuteCoroutine(coroutine);
        }
    }
}
