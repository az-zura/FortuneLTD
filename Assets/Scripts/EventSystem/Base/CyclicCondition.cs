using System.Collections.Generic;

namespace EventSystem.Base
{
    public class CyclicCondition: EventCondition
    {
        private List<ActionExecuteCondition> actionExecuteConditions = new List<ActionExecuteCondition>();
        private float cyclicWaitTime = 1.0f;
        
        
        public CyclicCondition(float cyclicWaitTime)
        {
            this.cyclicWaitTime = cyclicWaitTime;
        }
        
        
        //adds a condition for finalizing the condition
        public CyclicCondition AddCondition(ActionExecuteCondition actionExecuteCondition)
        {
            actionExecuteConditions.Add(actionExecuteCondition);
            return this;
        }
        
        
        public void CheckConditions()
        {
            bool canRun = true;
            foreach (ActionExecuteCondition condition in actionExecuteConditions)
            {
                canRun &= condition.EvaluateCondition();
            }

            if (canRun)
            {
                EndEventItem();
            }
            else
            {
                eventBase.SuspendEventItem(this,cyclicWaitTime);
            }
        }


        public override void OnItemStart()
        {
            CheckConditions();
        }

        public override void OnResumeExecution()
        {
            CheckConditions();
        }
    }
}