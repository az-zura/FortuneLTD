using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    //[SerializeField] private float speed;
    private float stopXLeft;
    private float stopXRight;
    private float startX;
    private bool isOpen = false;
    
    void OnTriggerEnter()
    {
        if (!isOpen)
        {
            Open();
            isOpen = true;
        }
    }

    /*
    private void OnTriggerExit()
    {
        if (isOpen)
        {
            Close();
            isOpen = false;
        }
    }
    */

    private void Open()
    {
        startX = transform.position.x;
        stopXLeft = transform.position.x + .8f;
        stopXRight = transform.position.x - .8f;
        if (isLeft)
        {
            while (transform.position.x < stopXLeft)
            {
                transform.position += new Vector3(.0001f, 0, 0);
            }
        }
        else
        {
            while (transform.position.x > stopXRight)
            {
                transform.position -= new Vector3(.0001f, 0, 0);

            }
        }
    }
/*
    private void Close()
    {
        if (isLeft)
        {
            while (transform.position.x > startX)
            {
                transform.position -= new Vector3(.0001f, 0, 0);
            }
        }
        else
        {
            while (transform.position.x < startX)
            {
                transform.position += new Vector3(.0001f, 0, 0);

            }
        }
    }
    */

   
}
