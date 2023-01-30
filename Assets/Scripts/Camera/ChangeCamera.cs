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
    [SerializeField] private GameLoop _gameLoop;

    private bool workingHours;

    private void Start()
    {
        workingHours = false;
        _gameLoop.WorkdayStarted += Working;
        _gameLoop.WorkdayEnded += WorkingEnded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((changeToDesk && !_miniGameLoop.HasRuleSheet()) ||!workingHours)
        {
            Debug.Log("Working Hours: " + workingHours + "\nRuleSheet: " + _miniGameLoop.HasRuleSheet() + "\nDesk: " + changeToDesk);
            return;
        }
        disableCamera.gameObject.SetActive(false);
        enableCamera.gameObject.SetActive(true);
        if(changeToDesk && workingHours) {
            _miniGameLoop.ActivateRulesheet();
            decke.SetActive(true);
            vents.SetActive(true);
            ruleSheetAccessoire.SetActive(false);
            _gameLoop.AtDesk = true;
        }
    }
    
    private void Working(object sender, EventArgs eventArgs)
    {
        workingHours = true;
    }

    private void WorkingEnded(object sender, EventArgs eventArgs)
    {
        workingHours = false;
    }

    
}
