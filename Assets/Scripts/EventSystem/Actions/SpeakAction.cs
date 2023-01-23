using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

public class SpeakAction : ActionBase
{
    private Speechbubble speechBubble;
    private GameObject speaker;
    private string text;
    private bool dismissAfterTime;
    private float time;

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
        AudioManager.instance.StopSound($"Voice{Random.Range(0,4)}");
        EndEventItem();
    }

    public override void OnItemStart()
    {
        speechBubble.SetBubble(text,Speechbubble.BubbleType.Speech,speaker.transform);
        AudioManager.instance.PlaySound($"Voice{Random.Range(0,4)}");
        
        speechBubble.btn.onClick.AddListener(() => { 
            dismissAfterTime = false;
            OnResumeExecution();
        });
        
        if (dismissAfterTime)
        {
            SuspendAction(time);
        }
    }
}
