using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem.Base;
using Unity.VisualScripting;
using UnityEngine;

public class ClickableItemCondition : InteractableObject
{

    public readonly TriggerCondition TriggerCondition = new();


    new void Start()
    {
        base.Start();
        onClick.AddListener(clicked);
    }

    private void clicked()
    {
        TriggerCondition.OnEventConditionMet();
    }

    
}
