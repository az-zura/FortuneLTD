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
    [Tooltip("Please set to false if more than one material is attached to the interactable Object.")] public bool highlightWhenSelected = true; //TODO maybe find a better fix for objects with more than one material

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
