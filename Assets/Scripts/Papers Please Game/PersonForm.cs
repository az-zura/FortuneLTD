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
        bauingenieur, arbeitslos
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

    public string GetName()
    {
        return name;
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
        string toCheck = abschluss.ToLower();
        if (toCheck.Contains("hauptschule"))
        {
            schulabschluss = eduaction.hauptschule;
        }
        else if (toCheck.Contains("realschule"))
        {
            schulabschluss = eduaction.realschule;
        }
        else if (toCheck.Contains("gymnasium"))
        {
            schulabschluss = eduaction.gymnasium;
        }
        else if (toCheck.Contains("ausbildung"))
        {
            schulabschluss = eduaction.ausbildung;
        }
        else if (toCheck.Contains("studium"))
        {
            schulabschluss = eduaction.studium;
        }
    }

    public void SetJob(string work)
    {
        string toCheck = work.ToLower();
        if (toCheck.Contains("bauingenieur"))
        {
            this.work = job.bauingenieur;
        }
        else
        {
            
        }
    }


}
