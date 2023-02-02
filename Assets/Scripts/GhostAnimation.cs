using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimation : MonoBehaviour
{
    public CharacterController controller;
    public float idleBobHeight = 0.4f;
    private Quaternion rotationDefault;
    private Animator animator;

    private Transform lookAtTransform;

    public enum Emotion
    {
        Default,
        Happy,
        Sad,
        Angry,
        Sceptical,
        Surprised,
        Thinking
    }

    private void Start()
    {
        
        animator = gameObject.GetComponent<Animator>();
        var r = gameObject.transform.localRotation;
        rotationDefault = new Quaternion(r.x,r.y,r.z,r.w);
    }

    void Update()
    {
        if (controller)
        {
            Vector3 v = new Vector3(controller.velocity.x, 0, controller.velocity.z).normalized;
            if (!(v.x == 0 && v.z == 0))
            {
                gameObject.transform.forward = -v;
            }
        }
        else
        {
            if (lookAtTransform)
            {
                Vector3 v = (lookAtTransform.position - transform.position);
                v.y = 0;
                v = -v.normalized;
                gameObject.transform.forward = Vector3.Lerp(gameObject.transform.forward, v, 0.8f);
            }
        }


        var o = gameObject;
        var localPosition = o.transform.localPosition;
        localPosition = new Vector3(localPosition.x, Mathf.Cos(Time.time) * idleBobHeight, localPosition.z);
        o.transform.localPosition = localPosition;
    }



    public void startLookAt(Transform lookAt)
    {
        this.lookAtTransform = lookAt;
    }

    public void stopLookAt()
    {
        this.lookAtTransform = null;
        this.transform.localRotation = rotationDefault;
    }
    
    public void setMoving(bool isMoving)
    {
        animator.SetBool("IsFloating", isMoving);
    }

    public void setEmotion(Emotion emotion)
    {
        Debug.Log("change emotion to " + emotion);
        animator.SetInteger("Emotion", emotion.GetHashCode());
    }
}