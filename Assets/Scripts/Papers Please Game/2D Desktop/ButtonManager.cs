using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private MonitorManager _monitorManager;

    public void OpenFamilyPic()
    {
        AudioManager.instance.PlaySound("Click");
        _monitorManager.OpenImage();
    }

    public void CloseFamilyPic()
    {
        AudioManager.instance.PlaySound("Click");
        _monitorManager.CloseImage();
    }

    public void OpenDeathxcel()
    {
        AudioManager.instance.PlaySound("Click");
        _monitorManager.OpenDeathxcel();
    }

    public void CloseDeathxcel()
    {
        AudioManager.instance.PlaySound("Click");
        _monitorManager.CloseDeathxcel();
    }

    public void AddPerson(TMP_InputField identificationNr)
    {
        AudioManager.instance.PlaySound("Click");
        if (!(identificationNr.text == String.Empty))
        {
            if (_monitorManager.AddPersonToDataBase(new PersonForm(identificationNr.text)))
            {
                _monitorManager.SwitchToFormWindow();
            }
        }      
    }
}
