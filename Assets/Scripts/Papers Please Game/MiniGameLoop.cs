using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameLoop : MonoBehaviour
{
    private List<Person> allPersons;
    [SerializeField] private DeskManager _deskManager;
    private Person toworkon;

    // Start is called before the first frame update
    void Start()
    {
        allPersons = Person.InstantiatePersons();
        StartWorkDay();
    }

    private void StartWorkDay()
    {
        toworkon = getRandomPerson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*
     * todo:
     * -get current person to do
     * 
     */

    private Person getRandomPerson()
    {
        int randomInt;
        do
        {
            randomInt = (int)Random.Range(0, 1);
        } while (allPersons[randomInt].IsDead());

        return allPersons[randomInt];
    }

    public Person GetPersonToWorkOn()
    {
        return toworkon;
    }
    
}
