using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimation : MonoBehaviour
{
    public CharacterController controller;
    public float idleBobHeight = 0.4f;

    private Animator animator;

    public Transform lookAt;

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
            if (lookAt)
            {
                Vector3 v = (lookAt.position - transform.position);
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

    public Transform LookAt
    {
        get => lookAt;
        set => lookAt = value;
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