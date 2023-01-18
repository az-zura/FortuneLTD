using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PersonForm
{
    private enum eduaction
    {
        hauptschule, realschule, gymnasium, ausbildung, studium
    }

    private enum job
    {
        bauingenieur
    }
    
    private string identificationNr;
    private string name;
    private int lifeExpectancy;
    private eduaction schulabschluss;
    private job work;
    private string specialNotes;

    public PersonForm(string id)
    {
        identificationNr = id;
    }

    public string GetID()
    {
        return identificationNr;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetLifeExpectancy(int years)
    {
        lifeExpectancy = years;
    }

    public void SetSchulabschluss(string abschluss)
    {
        
    }
    



}
