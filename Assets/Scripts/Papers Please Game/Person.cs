using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Person
{
    public enum Jobs
    {
        bauingenieur, sekretaer, anwalt, kunstlehrer, musiklehrer, 
        politiker, krankenpfleger, gaertner, farmer, kunststudent, 
        fliessbandarbeiter,  kanzleiangestellter, grafikdesigner,  
        strassenkehrer, 
        schueler, student, rente, arbeitslos
    }
    public enum Bildungsstand
    {
        keinAbschluss, grundschule, hauptschule, realschule, gymnasium, ausbildung, studium
    }
    
    private string _name;
    private string _identifikation;
    private int _derzeitigesAlter, _erwartetesAlter;
    private Jobs _job;
    private Bildungsstand _bildungsstand;

    private bool isDead;
    private Akte _akte;

    

    #region GetterSetter
    public bool IsDead()
        {
            return isDead;
        }

    public string GetName()
    {
        return _name;
    }

    public string GetIdentifikation()
    {
        return _identifikation;
    }

    public Akte GetAkte()
    {
        return _akte;
    }

        #endregion
    
    public Person(string name, string identifikation, int derzeitigesAlter, int erwartetesAlter, Jobs job, Bildungsstand bildungsstand) //GameObject akte
    {
        _name = name;
        _identifikation = identifikation;
        _derzeitigesAlter = derzeitigesAlter;
        _erwartetesAlter = erwartetesAlter;
        _job = job;
        _bildungsstand = bildungsstand;

        //_akte = new Akte(akte);
        isDead = false;
    }
    
    public static List<Person> InstantiatePersons()
    {
        List<Person> persons = new List<Person>();
        
        persons.Add(new Person("Seth Steinsburgh", "AGY-3482F", 42, 80, Jobs.bauingenieur, Bildungsstand.studium)); //akte fertig
        persons.Add(new Person("Joleen Wolfs", "KSH-8927Z", 70, 72, Jobs.rente, Bildungsstand.ausbildung)); //bild fertig
        persons.Add(new Person("Kevin Peterson", "LWS-1023G", 21, 65, Jobs.arbeitslos, Bildungsstand.keinAbschluss)); //bild fertig
        persons.Add(new Person("Isabel Kelly", "YTP-3294U", 20, 95, Jobs.student, Bildungsstand.gymnasium)); //bild fertig
        persons.Add(new Person("John Deer", "WRD-3012Q", 70, 79, Jobs.rente, Bildungsstand.ausbildung)); //bild fertig
        persons.Add(new Person("Isabel Wellinger", "XSO-0192Y", 50, 81, Jobs.kunstlehrer, Bildungsstand.studium)); //bild fertig
        persons.Add(new Person("David Bernhard", "KPF-6547J", 35, 75, Jobs.anwalt, Bildungsstand.studium)); //bild fertig
        persons.Add(new Person("Ruth Bradshaw",  "AKQ-9384H", 36, 74, Jobs.grafikdesigner, Bildungsstand.studium)); //bild fertig
        persons.Add(new Person("Peter Parkson", "ENV-0983G", 62, 69, Jobs.arbeitslos, Bildungsstand.hauptschule)); //bild fertig
        persons.Add(new Person("Jessica Sarah", "KXM-7717M", 32, 94, Jobs.musiklehrer, Bildungsstand.studium)); //bild fertig
        persons.Add(new Person("Quintus Batiatus", "JSA-1232M", 18, 76, Jobs.schueler, Bildungsstand.gymnasium)); //bild fertig
        persons.Add(new Person("Susanne Stiller", "QPA-0187S", 42, 67, Jobs.politiker, Bildungsstand.studium)); //bild fertig
        persons.Add(new Person("Marc Geller", "IUS-7612K", 47, 55, Jobs.krankenpfleger, Bildungsstand.ausbildung)); //bild fertig
        persons.Add(new Person("Erika Broll", "GHS-0937N", 24, 65, Jobs.gaertner, Bildungsstand.ausbildung)); //bild fertig
        persons.Add(new Person("Fred Mars", "TMZ-0874H", 22, 88, Jobs.farmer, Bildungsstand.hauptschule)); //bild fertig
        persons.Add(new Person("Jackie Goldberg", "LOP-8903W", 19, 70, Jobs.student, Bildungsstand.gymnasium)); //bild fertig
        persons.Add(new Person("Jack Russ", "GBD-2103J", 55, 67, Jobs.fliessbandarbeiter, Bildungsstand.hauptschule)); //bild fertig
        persons.Add(new Person("Hannelore Hugh", "MXR-1037U", 85, 87, Jobs.rente, Bildungsstand.studium)); //bild fertig
        persons.Add(new Person("Bernd Frueh", "LJY-6583I", 68, 71, Jobs.strassenkehrer, Bildungsstand.studium)); //bild fertig
        persons.Add(new Person("Ella Lou", "ITR-9435O", 23, 75, Jobs.student, Bildungsstand.gymnasium)); //bild fertig
        
        return persons;
    }
    
}