using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public abstract class ActionBase
    {

        private EventBase eventBase;
        private List<ActionExecuteCondition> actionExecuteConditions = new List<ActionExecuteCondition>();

        //calls event system to suspend execution
        public void SuspendAction(float timeInSeconds)
        {
            eventBase.SuspendAction(this,timeInSeconds);
        }

        //adds a condition to the execution of this action
        public ActionBase AddCondition(ActionExecuteCondition actionExecuteCondition)
        {
            actionExecuteConditions.Add(actionExecuteCondition);
            return this;
        }

        //called by the event system to resume execution
        public abstract void OnResumeExecution();
        
        //Called by event system
        public void DispatchAction(EventBase eventBase)
        {
            this.eventBase = eventBase;
            this.OnActionStart();
        }

        //gets called by the event system, when this returns true, OnActionStart is called by the event system
        public bool CanActionRun()
        {
            bool canRun = true;
            foreach (ActionExecuteCondition condition in actionExecuteConditions)
            {
                canRun &= condition.EvaluateCondition();
            }

            return canRun;
        }
        
        //gets called by the event system when the action is played
        public abstract void OnActionStart();
    
        //has to be called by the script to hand control over to the event System
        public void EndAction()
        {
            eventBase.ActionRanCallback(this);
        }

    }
}
