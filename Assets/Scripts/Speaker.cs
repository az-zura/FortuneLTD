using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Speaker
{
    public GameObject gameObj;
    public string[][] texts;
    private int curInteraction = 0;
    private int curTextPart = 0;
    private int id; // Please make sure that each speaker has a unique id!

    public Speaker(int id, string[][] texts)
    {
        this.texts = texts;
        this.id = id;
        
        curInteraction = PlayerPrefs.GetInt($"alreadySaid{id}", 0);
    }
    
    public Speaker(string[][] texts)
    {
        this.texts = texts;
    }

    public void SetSpeakerID(int id)
    {
        this.id = id;
        
        curInteraction = PlayerPrefs.GetInt($"alreadySaid{id}", 0);
    }
    
    public int GetSpeakerID()
    {
        return id;
    }
    
    public string GetNextText()
    {
        if (curInteraction >= texts.Length)
        {
            return "";
        }
        
        string s = texts[curInteraction][curTextPart];
        curTextPart++;
        if (curTextPart >= texts[curInteraction].Length)
        {
            curTextPart = 0;
            curInteraction++;
            PlayerPrefs.SetInt($"alreadySaid{id}", curInteraction);
        }
        return s;
    }
}
