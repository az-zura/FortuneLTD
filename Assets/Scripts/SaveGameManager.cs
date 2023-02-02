using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager instance;
    [SerializeField] private GameLoop gameLoop;
    [SerializeField] private Transform player;
    
    private List<string> eventsCompleted = new List<string>();
    
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        
        LoadGame();
    }

    public void LoadGame()
    {
        // Time
        gameLoop.timePassedToday = GetSavedTimePassedToday();
        gameLoop.currentDay = GetSavedDay();
        
        // PlayerPos
        player.position = GetSavedPosition();
        
        // events are loaded from the event scripts with WasEventCompleted
    }

    public void SaveGame()
    {
        // Time
        PlayerPrefs.SetFloat("TimePassedToday", gameLoop.timePassedToday);
        PlayerPrefs.SetInt("Day", gameLoop.currentDay);
        Debug.Log($"time saved: {gameLoop.timePassedToday}, day saved: {gameLoop.currentDay}");
        
        // PlayerPos
        PlayerPrefs.SetFloat("posX", player.position.x);
        PlayerPrefs.SetFloat("posY", player.position.y);
        PlayerPrefs.SetFloat("posZ", player.position.z);
        Debug.Log($"position saved: {player.position}");
        
        // Events
        foreach (var e in eventsCompleted)
        {
            Debug.Log($"event saved: {e}");
            PlayerPrefs.SetInt(e, 1);
        }
        
        eventsCompleted.Clear();
    }

    public int GetSavedDay()
    {
        return PlayerPrefs.GetInt("Day", 0);
    }
    
    public float GetSavedTimePassedToday()
    {
        return PlayerPrefs.GetFloat("TimePassedToday", 0);
    }
    
    public Vector3 GetSavedPosition()
    {
        float x = PlayerPrefs.GetFloat("posX", 0);
        float y = PlayerPrefs.GetFloat("posY", 1.3f);
        float z = PlayerPrefs.GetFloat("posZ", 0);
        return new Vector3(x, y, z);
    }

    public bool WasEventCompleted(string eventId)
    {
        return PlayerPrefs.GetInt($"Complete{eventId}", 0) == 1;
    }
    
    public void AddCompletedEvent(string eventId)
    {
        Debug.Log($"eventId added: {eventId}");
        eventsCompleted.Add($"Complete{eventId}");
    }
}
