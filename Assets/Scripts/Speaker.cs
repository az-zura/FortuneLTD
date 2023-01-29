using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Speaker
{
    public GameObject gameObj;
    public string[][] texts;
    private int curInteraction = 0;
    private int curTextPart = 0;

    public Speaker(string[][] texts)
    {
        this.texts = texts;
    }
    
    public string GetNextText()
    {
        string s = texts[curInteraction][curTextPart];
        curTextPart++;
        if (curTextPart >= texts[curInteraction].Length)
        {
            curTextPart = 0;
            curInteraction = Random.Range(0, texts.Length);
        }
        return s;
    }
    
    public string GetRandomText()
    {
        curInteraction = Random.Range(0, texts.Length);
        return texts[curInteraction][curTextPart];
    }
}
