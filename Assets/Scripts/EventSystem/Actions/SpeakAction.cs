using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;
using UnityEngine.Events;

public class SpeakAction : ActionBase
{
    private Speechbubble speechBubble;
    private GameObject speaker;
    private string text;
    private bool dismissAfterTime;
    private float time;

    private int voiceType;

    private UnityAction action;

    public SpeakAction(Speechbubble speechBubble, GameObject speaker , string text, float time = default)
    {
        if (time == default)
        {
            dismissAfterTime = false;
        }
        else
        {
            dismissAfterTime = true;
            this.time = time;
        }
        
        this.speechBubble = speechBubble;
        this.speaker = speaker;
        this.text = text;
    }
    
    
    public override void OnResumeExecution()
    {
        speechBubble.ChangeBubbleType(Speechbubble.BubbleType.Dismissed);
        AudioManager.instance.StopSound($"Voice{voiceType}");
        speechBubble.btn.onClick.RemoveListener(Action);
        Debug.Log("end speek action with text : " + text);
        EndEventItem();
    }
    
    public override void OnItemStart()
    {
        speechBubble.SetBubble(text,Speechbubble.BubbleType.Speech,speaker.transform);
        voiceType = Random.Range(0, 4);
        AudioManager.instance.PlaySound($"Voice{voiceType}");
        speechBubble.btn.onClick.AddListener(Action);
        if (dismissAfterTime)
        {
            Debug.Log("dismissed after time ");
            SuspendAction(time);
        }
    }

    private void Action()
    {
        Debug.Log("register click");
        dismissAfterTime = false;
        OnResumeExecution();
    }
}
