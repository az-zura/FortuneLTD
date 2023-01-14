using System.Collections;
using UnityEngine;

namespace EventSystem
{
    public abstract class EventBase : MonoBehaviour
    {
        public float cyclicWaitInterval = 1f;
        //called by the event trigger when event starts
        public abstract void InitEvent();
        
        public void DispatchAction(ActionBase action)
        {
            if (action.CanActionRun())
            {
                action.DispatchAction(this);
            }
            else
            {
                StartCoroutine(CyclicWait(action));
            }
        }
        public abstract void ActionRanCallback(ActionBase action);

        public void SuspendAction(ActionBase action, float timeInSeconds)
        {
            StartCoroutine(Suspend(action, timeInSeconds));
        }
        
        //can be called by action
        public void ExecuteCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
        
        IEnumerator Suspend(ActionBase action,float timeInSeconds)
        {
            yield return new WaitForSeconds(timeInSeconds);
            action.OnResumeExecution();
        }

        IEnumerator CyclicWait(ActionBase action)
        {
            yield return new WaitForSeconds(cyclicWaitInterval);
            DispatchAction(action);

        }
    }
}
