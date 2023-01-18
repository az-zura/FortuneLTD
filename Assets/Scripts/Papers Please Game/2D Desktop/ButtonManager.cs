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
        _monitorManager.OpenImage();
    }

    public void CloseFamilyPic()
    {
        _monitorManager.CloseImage();
    }

    public void OpenDeathxcel()
    {
        _monitorManager.OpenDeathxcel();
    }

    public void CloseDeathxcel()
    {
        _monitorManager.CloseDeathxcel();
    }

    public void AddPerson(TMP_InputField identificationNr)
    {
        if (!(identificationNr.text == String.Empty))
        {
            if (_monitorManager.AddPersonToDataBase(new PersonForm(identificationNr.text)))
            {
                _monitorManager.SwitchToFormWindow();
            }
        }      
    }

    public void FillOutPersonInformation()
    {
        
        
    }

    public void FillOutPersonInformationName(TMP_InputField name) 
    {
        
    }
    
    public void FillOutPersonInformationLifeTime(TMP_InputField lifetime) 
    {
        
    }
    
    public void FillOutPersonInformationSchulabschluss(TMP_InputField schulabschluss) 
    {
        
    }
    
    public void FillOutPersonInformationJob(TMP_InputField job) 
    {
        
    }
    
}