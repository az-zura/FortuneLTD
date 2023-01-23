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

    public void Start()
    {
        transform.tag = SelectionManager.selectableTag;
        SelectionManager.instance.interactableObjs.Add(transform);
    }

    private void OnDestroy()
    {
        SelectionManager.instance.interactableObjs.Remove(transform);
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
