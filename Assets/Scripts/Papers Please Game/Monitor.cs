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

    public bool AddPerson(PersonForm personForm)
    {
        if (!PersonAlreadyAdded(personForm))
        {
            personsData.Add(personForm);
            return true;
        }

        return false;
    }

    public bool PersonAlreadyAdded(PersonForm personForm)
    {
        foreach (PersonForm pF in personsData)
        {
            if (pF.GetID().Equals(personForm.GetID()))
            {
                return true;
            }
        }

        return false;
    }

    public PersonForm FindPersonByID(string id)
    {
        foreach (var person in personsData)
        {
            if (person.GetID().Equals(id))
            {
                return person;
            }
        }

        return null;
    }
    
}
