using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEngine;

public class RuleSheetTrigger : MonoBehaviour
{
    [SerializeField] private MiniGameLoop _miniGameLoop;
    [SerializeField] private GameObject rulesheet;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("WTF");
        _miniGameLoop.SetHasRuleSheet(true);
        rulesheet.SetActive(true);
    }
}
