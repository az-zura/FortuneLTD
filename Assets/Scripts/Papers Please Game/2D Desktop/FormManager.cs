using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FormManager : MonoBehaviour
{
    [SerializeField] private MonitorManager _manager;
    [SerializeField] private TMP_InputField name;
    [SerializeField] private TMP_InputField lifetime;
    [SerializeField] private TMP_InputField education;
    [SerializeField] private TMP_InputField job;


    public void FillOutForm()
    {
        if (name.text == String.Empty || lifetime.text == String.Empty || education.text == String.Empty || job.text == String.Empty)
        {
            return;
        }
        _manager.AddDataToPerson(name.text,  Int32.Parse(lifetime.text), education.text, job.text);
        _manager.SwitchToCalculator();
    }
    
}
