using System;
using System.Collections;
using EventSystem.Base;
using UnityEngine;
using Object = System.Object;

namespace EventSystem
{
    public abstract class EventBase : MonoBehaviour
    {
        public string uniqueEventName; // IMPORTANT for saving the game
        
        enum EventStatus {
            EventNew,
            EventPlayed,
            EventPlaying
        }

        private EventStatus status = EventStatus.EventNew;
        public abstract void OnEventInitialized();

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SaveGameManager.instance != null);
            if (uniqueEventName == "")
                Debug.Log(gameObject.name);
            if (SaveGameManager.instance.WasEventCompleted(uniqueEventName))
            {
                status = EventStatus.EventPlayed;
            }
        }

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
            if (uniqueEventName != "")
            {
                SaveGameManager.instance.AddCompletedEvent(uniqueEventName);
            }
            else
            {
                Debug.LogWarning("Event can not be saved because the eventName is empty.");
            }
        }
        
        public void DispatchEventItem(EventItem eventItem)
        {
            eventItem.DispatchItem(this);
        }
        
        public abstract void EventItemRanCallback(EventItem eventItem);

        public void SuspendEventItem(EventItem eventItem, float timeInSeconds)
        {
            StartCoroutine(Suspend(eventItem, timeInSeconds));
        }
        
        //can be called by action
        public void ExecuteCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
        
        IEnumerator Suspend(EventItem eventItem,float timeInSeconds)
        {
            yield return new WaitForSeconds(timeInSeconds);
            eventItem.OnResumeExecution();
        }

        public GameObject InstantiateActor(GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            return Instantiate(gameObject,position,rotation);
        }

        public void DestroyActor(GameObject gameObject)
        {
            Destroy(gameObject);
        }
    }
}
