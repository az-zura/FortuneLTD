using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager instance;
    [SerializeField] private GameLoop gameLoop;
    [SerializeField] private Transform player;
    [SerializeField] private MainQuest mainQuest;
    
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
        Debug.Log("Load game");
        // Time
        gameLoop.timePassedToday = GetSavedTimePassedToday();
        gameLoop.currentDay = GetSavedDay();
        if (gameLoop.GetHour() >= 21 || gameLoop.GetHour() < 5)
        {
            gameLoop.OnWorkdayStarted();
        }

        // PlayerPos
        player.position = GetSavedPosition();

        // events are loaded from the event scripts with WasEventCompleted
        // main quest state is loaded in Main Quest
    }

    public void SaveGame()
    {
        // Time
        SaveDayAndTime();
        Debug.Log($"time saved: {gameLoop.timePassedToday}, day saved: {gameLoop.currentDay}");
        
        // PlayerPos
        SavePosition();
        Debug.Log($"position saved: {player.position}");
        
        // Events
        foreach (var e in eventsCompleted)
        {
            SaveEvent(e);
            Debug.Log($"event saved: {e}");
        }
        
        eventsCompleted.Clear();
        
        // Main Quest state
        SaveMainQuestState(mainQuest.mainQuestState);
    }

    public int GetSavedDay()
    {
        return PlayerPrefs.GetInt("Day", 0);
    }
    
    public float GetSavedTimePassedToday()
    {
        return PlayerPrefs.GetFloat("TimePassedToday", 17);
    }
    
    public Vector3 GetSavedPosition()
    {
        float x = PlayerPrefs.GetFloat("posX", player.position.x);
        float y = PlayerPrefs.GetFloat("posY", player.position.y);
        float z = PlayerPrefs.GetFloat("posZ", player.position.z);
        return new Vector3(x, y, z);
    }

    public bool WasEventCompleted(string eventId)
    {
        return PlayerPrefs.GetInt($"Complete{eventId}", 0) == 1;
    }

    public int GetSavedMainQuestState()
    {
        return PlayerPrefs.GetInt("MainQuestState", 0);
    }
    
    public void AddCompletedEvent(string eventId)
    {
        Debug.Log($"eventId added: {eventId}");
        eventsCompleted.Add($"Complete{eventId}");
    }

    public void SaveEvent(string eventId)
    {
        PlayerPrefs.SetInt($"Complete{eventId}", 1);
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("posX", player.position.x);
        PlayerPrefs.SetFloat("posY", player.position.y);
        PlayerPrefs.SetFloat("posZ", player.position.z);
    }

    public void SaveDayAndTime()
    {
        SaveDay();
        SaveTime();
    }
    
    public void SaveDay()
    {
        PlayerPrefs.SetInt("Day", gameLoop.currentDay);
    }
    
    public void SaveTime()
    {
        PlayerPrefs.SetFloat("TimePassedToday", gameLoop.timePassedToday);
    }

    public void SaveMainQuestState(int mainQuestState)
    {
        PlayerPrefs.SetInt("MainQuestState", mainQuestState);
    }

    public void ResetCompletely()
    {
        PlayerPrefs.DeleteAll();
        gameLoop.timePassedToday = 17; // TODO this is only a quick fix - think of sth nicer

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
