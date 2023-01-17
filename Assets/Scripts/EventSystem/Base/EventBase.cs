using System;
using System.Collections;
using UnityEngine;

namespace EventSystem
{
    public abstract class EventBase : MonoBehaviour
    {
        enum EventStatus {
            EventNew,
            EventPlayed,
            EventPlaying
        }

        private EventStatus status = EventStatus.EventNew;
        public float cyclicWaitInterval = 1f;
        public abstract void OnEventInitialized();

        //called by the event trigger when event starts
        public void InitializeEvent()
        {
            if (this.status == EventStatus.EventNew)
            {
                this.status = EventStatus.EventPlaying;
                this.OnEventInitialized();
            }
            else
            {
                if (status == EventStatus.EventPlaying) Debug.Log("Event is already in action");
                if (status == EventStatus.EventPlayed) Debug.Log("Event was already executed once");
            }
        }

        public void finalizeEvent()
        {
            Debug.Log("Finalizing event");
            this.status = EventStatus.EventPlayed;
        }
        
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
