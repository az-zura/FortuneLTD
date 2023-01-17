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
        SelectionManager.instance.interactableObjs.Add(transform);
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
