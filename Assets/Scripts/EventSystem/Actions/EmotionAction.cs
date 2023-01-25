using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

public class EmotionAction : ActionBase
{
    private GhostAnimation ghostAnimation;
    private GhostAnimation.Emotion emotion;
    private float defferTime;

    public EmotionAction(GhostAnimation ghostAnimation, GhostAnimation.Emotion emotion, float defferTime = 0.0f)
    {
        this.ghostAnimation = ghostAnimation;
        this.emotion = emotion;
        this.defferTime = defferTime;
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }

    public override void OnItemStart()
    {
        DispatchCoroutine(animate(ghostAnimation,emotion,defferTime));
        EndEventItem();
    }

    private static IEnumerator animate(GhostAnimation ghostAnimation, GhostAnimation.Emotion emotion, float defferTime)
    {
        yield return new WaitForSeconds(defferTime);
        ghostAnimation.setEmotion(emotion);
    }
}
