using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonitorManager : MonoBehaviour
{
    private Monitor _monitor;
    [SerializeField] private GameObject desktop;
    [SerializeField] private GameObject deathxcel;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject firstWindow;
    [SerializeField] private GameObject formWindow;

    [SerializeField] private TextMeshProUGUI idText;
    
    private PersonForm currentPerson;

    //mouse click sounds
    private void Start()
    {
        _monitor = new Monitor();
    }

    public void OpenImage()
    {
        desktop.SetActive(false);
        image.SetActive(true);
    }
    public void CloseImage()
    {
        image.SetActive(false);
        desktop.SetActive(true);
    }
    public void OpenDeathxcel()
    {
        deathxcel.SetActive(true);
        desktop.SetActive(false);
    }
    public void CloseDeathxcel()
    {
        deathxcel.SetActive(false);
        desktop.SetActive(true);
    }
    
    public bool AddPersonToDataBase(PersonForm personForm)
    {
        if (_monitor.AddPerson(personForm))
        {
            currentPerson = personForm;
            return true;
        }

        return false;
    }

    public void SwitchToFormWindow()
    {
        formWindow.SetActive(true);
        firstWindow.SetActive(false);
        //Debug.Log(currentPerson.GetID());
        string toAdd = "    " + currentPerson.GetID();
        idText.text += toAdd;
    }

    
    //add rest of data to person via second window:
}
