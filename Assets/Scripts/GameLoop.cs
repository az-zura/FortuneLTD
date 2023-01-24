using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private int currentDay;
    private float timePassedToday = 0;

    public float oneHourIsEquivalentToXSeconds = 60;

    public event EventHandler HourUpdated;
    public event EventHandler DayUpdated;

    private void Start()
    {
        currentDay = 0;
    }

    private void Update()
    {
        //update time
        float timePreUpdate = timePassedToday;
        timePassedToday += Time.deltaTime / oneHourIsEquivalentToXSeconds;

        if ((int)timePreUpdate != (int)timePassedToday && (int)timePassedToday != 0)
        {
            OnNewHour((int)timePassedToday);
        }
        
    }

    private void OnNewHour(int hour)
    {
        Debug.Log("current hour: [" + hour + ":00]");
        OnHourUpdated();
        if (hour == 24)
        {
            OnNewDay();
        }
    }

    private void OnNewDay()
    {
        currentDay++;
        timePassedToday = 0;
        OnDayUpdated();
        Debug.Log("Day Nr "+currentDay+" has begun");
    }

    public float GetTime()
    {
        return timePassedToday;
    }

    public int GetHour()
    {
        return (int)timePassedToday;
    }

    public int GetDay()
    {
        return currentDay;
    }

    //events 
    protected virtual void OnHourUpdated()
    {
        HourUpdated?.Invoke(this,EventArgs.Empty);
    }

    protected virtual void OnDayUpdated()
    {
        DayUpdated?.Invoke(this, EventArgs.Empty);
    }
}
