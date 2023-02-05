using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [HideInInspector] public int currentDay;
    [HideInInspector] public float timePassedToday = 19;

    public int workDayStart = 21;
    public int workDayEnd = 5;

    public float oneHourIsEquivalentToXSeconds = 60;

    public event EventHandler HourUpdated;
    public event EventHandler DayUpdated;
    public event EventHandler WorkdayStarted;
    public event EventHandler WorkdayEnded;
    public bool WorkdayEndedEarly;

    public bool AtDesk;

    private bool isWorkingTime = false;

    private float dontSurpassTime = -1;
    private float fastForwardHours = -1;
    public float dontSurpassHalt = 0.5f;
    public float fastForwardMultiplier = 35;

    public HologramPath pathfinding;
    public Transform officeTarget;
    public Transform player;
    private bool isInOffice = false;
    
    private void Start()
    {
        WorkdayEndedEarly = false;
        currentDay = 0;
        AtDesk = false;
    }

    private void Update()
    {
        //update time
        float multiplier = 1;
        if (dontSurpassTime > -1 && Math.Abs(dontSurpassTime - timePassedToday) < dontSurpassHalt)
        {
            multiplier = (dontSurpassTime < timePassedToday)? 0 : (dontSurpassTime - timePassedToday) * 1 / dontSurpassHalt;
        }

        if (fastForwardHours > 0)
        {
            multiplier = fastForwardMultiplier;
        }
        float timePreUpdate = timePassedToday;
        var d =  (Time.deltaTime / oneHourIsEquivalentToXSeconds) * multiplier;
        timePassedToday += d;
        if (fastForwardHours > 0)
        {
            fastForwardHours -= d;
        }
        if ((int)timePreUpdate != (int)timePassedToday && (int)timePassedToday != 0)
        {
            OnNewHour((int)timePassedToday);
        }

        if (isWorkingTime && !isInOffice)
        {
            pathfinding.gameObject.SetActive(true);
            pathfinding.target = officeTarget;
        }

        SaveGameManager.instance.SaveTime();
    }

    public void setTime(int hour)
    {
        timePassedToday = hour;
        OnNewHour((int)timePassedToday);
    }

    public void setDontSurpass(float time)
    {
        this.dontSurpassTime = time;
    }

    public void setFastForwardUntil(float targetHour)
    {
        if (targetHour < timePassedToday)
        {
            setFastForwardHours(24 - timePassedToday + targetHour);
        }
        else
        {
            setFastForwardHours(targetHour - timePassedToday);
        }
    }
    public void setFastForwardHours(float time)
    {
        Debug.Log("forward : +"+time);
        this.fastForwardHours = time;
    }

    public void clearDontSurpass()
    {
        dontSurpassTime = -1;
    }

    private void OnNewHour(int hour)
    {
        Debug.Log("current hour: [" + hour + ":00]");
        OnHourUpdated();

        switch (hour)
        {
          case 24: OnNewDay();
              break;
          case 21: OnWorkdayStarted();
              break;
          case 5:
              if (!WorkdayEndedEarly) OnWorkdayEnded();
              break;
        }
    }

    private void OnNewDay()
    {
        currentDay++;
        timePassedToday = 0;
        OnDayUpdated();
        Debug.Log("Day Nr "+currentDay+" has begun");
    }

    public bool IsWorkingTime()
    {
        return isWorkingTime;
    }
    
    public float GetTime()
    {
        return timePassedToday;
    }

    public int GetHour()
    {
        return (int)timePassedToday;
    }
    
    public int GetMinutes()
    {
        return (int) ((timePassedToday - GetHour()) * 60);
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
        SaveGameManager.instance.SaveDay();
        DayUpdated?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnWorkdayStarted()
    {
        isWorkingTime = true;

        WorkdayStarted?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnWorkdayEnded()
    {
        isWorkingTime = false;
        pathfinding.gameObject.SetActive(false);
        
        WorkdayEnded?.Invoke(this, EventArgs.Empty);
    }

    public void SetInOffice(bool inOffice)
    {
        isInOffice = inOffice;
    }
}
