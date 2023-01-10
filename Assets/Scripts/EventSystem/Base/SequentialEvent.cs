using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public abstract class SequentialEvent : EventBase
    {
        private List<ActionBase> actions = new List<ActionBase>();
        private int currentAction = 0;

        public void AddAction(ActionBase action)
        {
            this.actions.Add(action);
        }
    
        public void SetActions(List<ActionBase> actions)
        {
            this.actions = actions;
        }


        public void StartSequentialEvent()
        {
            if (this.actions.Count <= 0)
            {
                Debug.LogError("Trying to start sequential event, but action array is empty");
            }
            else
            {
                DispatchAction(this.actions[0]);
            }
        }
        public override void ActionRanCallback(ActionBase action)
        {
            NextAction();
        }

        public void NextAction()
        {
            currentAction++;
            if (currentAction >= actions.Count)
            {
                Debug.Log("Last action has been played");
            }
            else
            {
                DispatchAction(actions[currentAction]);
            }
        }
    }
}
