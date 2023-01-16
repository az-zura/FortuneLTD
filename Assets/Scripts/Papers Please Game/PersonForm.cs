using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PersonForm
{
    private string identificationNr;
    private string name;
    private int lifeExpectancy;
    private string personalStatus; //single, married, widowed
    private string job;
    private string specialNotes;

    public PersonForm(string id)
    {
        identificationNr = id;
    }

    public string GetID()
    {
        return identificationNr;
    }


}
