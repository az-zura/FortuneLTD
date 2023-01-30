using System;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

namespace EventSystem
{
    public abstract class SequentialEvent : EventBase
    {
        private List<EventItem> eventItems = new List<EventItem>();
        private int currentAction = 0;

        public void AddEventItem(EventItem eventItem)
        {
            this.eventItems.Add(eventItem);
        }

        public void StartSequentialEvent()
        {
            if (this.eventItems.Count <= 0)
            {
                Debug.LogError("Trying to start sequential event, but action array is empty");
            }
            else
            {
                DispatchEventItem(this.eventItems[0]);
            }
        }
        public override void EventItemRanCallback(EventItem eventItem)
        {
            NextEventItem();
        }

        public void NextEventItem()
        {
            currentAction++;
            if (currentAction >= eventItems.Count)
            {
                finalizeEvent();
            }
            else
            {
                DispatchEventItem(eventItems[currentAction]);
            }
        }
    }
}

public class DispatchEventArgs : EventArgs
{
    public DispatchEventArgs(ActionBase actionBase)
    {
        this.actionBase = actionBase;
    }

    private ActionBase actionBase;

    public ActionBase ActionBase
    {
        get => actionBase;
        set => actionBase = value;
    }
}
