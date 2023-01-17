using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Renderer))]
public class InteractableObject : MonoBehaviour
{
    public UnityEvent onClick;

    private void Start()
    {
        transform.tag = SelectionManager.selectableTag;
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
