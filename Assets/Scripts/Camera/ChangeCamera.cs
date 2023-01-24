using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private Camera disableCamera;
    [SerializeField] private Camera enableCamera;

    [SerializeField] private bool changeToDesk;
    [SerializeField] private GameObject decke;
    [SerializeField] private GameObject vents;
    [SerializeField] private GameObject ruleSheetAccessoire;

    [SerializeField] private MiniGameLoop _miniGameLoop;
    private void OnTriggerEnter(Collider other)
    {
        if (changeToDesk && !_miniGameLoop.HasRuleSheet())
        {
            return;
        }
        disableCamera.gameObject.SetActive(false);
        enableCamera.gameObject.SetActive(true);
        if(changeToDesk) {
            decke.SetActive(true);
            vents.SetActive(true);
            ruleSheetAccessoire.SetActive(false);
        }
       
    }

    
}
