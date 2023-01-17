using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor
{
    /*
     * to dos:
     * open the program with the form (NTH)
     * fill out form
     * decide if person gets killed or not
     * close monitor again
     */
    private List<PersonForm> personsData;
    private List<string> personsIDs;

    public Monitor()
    {
        personsData = new List<PersonForm>();
        personsIDs = new List<string>();
    }

    public bool addPerson(string id)
    {
        if (!personAlreadyAdded(id))
        {
            personsData.Add(new PersonForm(id));
            return true;
        }

        return false;
    }

    public bool personAlreadyAdded(string id)
    {
        foreach (PersonForm personForm in personsData)
        {
            if (personForm.GetID().Equals(id))
            {
                return true;
            }
        }

        return false;
    }
    
    
    
}
