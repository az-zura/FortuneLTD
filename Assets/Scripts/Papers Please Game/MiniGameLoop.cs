using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


public class MiniGameLoop : MonoBehaviour
{
    private List<Person> allPersons;
    private List<Person> dailyPersons;
    private int numberOfPersons;
    private Person toworkon;
    private GameObject currentFolderGameObject;
    private state currentState;
    private List<Akte> akten;
    private List<Person.Jobs> dailyJobs;
    private int currentDay = 0;

    [SerializeField] private GameLoop _gameLoop;
    private enum state
    {
        desk, folder, rulesheet, monitor, pen
    }

    #region All game objects
        [SerializeField] private GameObject folderTray;
        private bool folderTrayEmpty;
        [SerializeField] private GameObject closedFolderOnDesk;
        [SerializeField] private GameObject openedFolderOnDesk;

        [SerializeField] private List<GameObject> foldersGameobjects;
        
        [SerializeField] private Canvas monitorUI;
        [SerializeField] private List<GameObject> firstPages;

        [SerializeField] private GameObject rulesheetui;
        [SerializeField] private TextMeshProUGUI job1text;
        [SerializeField] private TextMeshProUGUI job2text;
        [SerializeField] private List<GameObject> imagesJob1;
        [SerializeField] private List<GameObject> imagesJob2;


    #endregion

    #region Cameras
        [SerializeField] private Camera deskCamera;
        [SerializeField] private Camera folderCamera;
        [SerializeField] private Camera rulesheetCamera;
        #endregion

    #region  bools

        private bool secondPageJustOpened;
        private bool hasRulesheet;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentState = state.desk;
        allPersons = Person.InstantiatePersons();

        folderTrayEmpty = false;
        secondPageJustOpened = true;
        hasRulesheet = false;
        
        akten = new List<Akte>();
        dailyJobs = new List<Person.Jobs>();
        dailyPersons = new List<Person>();

        _gameLoop.DayUpdated += UpdateCurrentDay;
        GetAktenFromFolderGameObjects();
        StartWorkDay();
    }

    private void UpdateCurrentDay(object sender, EventArgs eventArgs)
    {
        currentDay++;
        EndWorkDay();
    }
    
    private void StartWorkDay()
    {
        numberOfPersons = Random.Range(4, 7);
        for (int i = 0; i < numberOfPersons; i++)
        {
            dailyPersons.Add(GetRandomPerson());
        }

        int personToWorkOn = Random.Range(0, numberOfPersons - 1);
        toworkon = dailyPersons[personToWorkOn];
        
        GetDailyJobs();
    }

    private void EndWorkDay()
    {
        dailyPersons.Clear();
        dailyJobs.Clear();
    }

    private void PersonFinished()
    {
        dailyPersons.Remove(toworkon);
    }

    private void GetDailyJobs()
    {
        Person.Jobs firstJob = Person.GetRandomJob();
        Person.Jobs secondJob;
        do
        {
            secondJob = Person.GetRandomJob();
        } while (firstJob.Equals(secondJob));
        
        dailyJobs.Add(firstJob);
        dailyJobs.Add(secondJob);
    }

    private Person GetRandomPerson()
    {
        int randomInt;
        do
        {
            randomInt = Random.Range(0, 20);
        } while (allPersons[randomInt].IsDead());

        return allPersons[randomInt];
    }

    public void ObjectHit(string objectName)
    {
        AudioManager.instance.PlaySound("Click");
        switch (objectName)
        {
            case "FolderTray":
            {
                if (currentState == state.desk)
                {
                    if (!closedFolderOnDesk.activeSelf && !folderTrayEmpty)
                    {
                        closedFolderOnDesk.gameObject.SetActive(true);
                    }
                }
                break;
            }
            case "FolderUnopened":
            {
                if (currentState == state.desk)
                {
                    openedFolderOnDesk.SetActive(true);
                    closedFolderOnDesk.SetActive(false);
                    //problem
                    GetCurrentFolderGameObjectToPerson().SetActive(true);
                    toworkon.GetAkte().InstantiateAkte(GetCurrentFolderGameObjectToPerson());
                    Debug.Log(toworkon.GetAkte().GetName());
                    deskCamera.gameObject.SetActive(false);
                    folderCamera.gameObject.SetActive(true);
                    currentState = state.folder;
                }
                break;
            }
            case "FirstPage":
            {
                if (currentState == state.folder)
                {
                    toworkon.GetAkte().DisableFirstPageAndImage();
                }
                break;
            }
            case "SecondPage":
            {
                if (currentState == state.folder)
                {
                    
                    if (!secondPageJustOpened || toworkon.GetAkte().isConfidential)
                    {
                        toworkon.GetAkte().EnableFirstPageAndImage();
                    }
                    else
                    {
                        toworkon.GetAkte().DisableSecondPage();
                    }
                    secondPageJustOpened = !secondPageJustOpened;
                }
                break;
            }
            case "ThirdPage":
            {
                if (currentState == state.folder)
                {
                    toworkon.GetAkte().EnableSecondPage();
                }
                break;
            }
            case "Monitor":
            {
                if (currentState == state.desk)
                {
                    monitorUI.gameObject.SetActive(true); //enable monitor UI
                }
                break;
            }
            case "RuleSheet":
            {
                if (currentState == state.desk)
                {
                    currentState = state.rulesheet;
                    rulesheetui.SetActive(true);
                    job1text.text = dailyJobs[0].ToString();
                    job2text.text = dailyJobs[1].ToString();
                    GetImageToJob(dailyJobs[0], true);
                    GetImageToJob(dailyJobs[1], false);

                    rulesheetCamera.gameObject.SetActive(true);
                }
                break;
            }
        }
    }

    private void GetImageToJob(Person.Jobs jobname, bool job1)
    {
        if (job1)
        {
            switch (jobname)
            {
                case Person.Jobs.strassenkehrer : imagesJob1[0].SetActive(true); break;
                case Person.Jobs.grafikdesigner : imagesJob1[1].SetActive(true); break;
                case Person.Jobs.bauingenieur : imagesJob1[2].SetActive(true); break;
                case Person.Jobs.farmer : imagesJob1[3].SetActive(true); break;
                case Person.Jobs.gaertner : imagesJob1[4].SetActive(true); break;
                case Person.Jobs.anwalt : imagesJob1[5].SetActive(true); break;
                case Person.Jobs.krankenpfleger : imagesJob1[6].SetActive(true); break;
                case Person.Jobs.politiker : imagesJob1[7].SetActive(true); break;
                case Person.Jobs.kunstlehrer : imagesJob1[8].SetActive(true); break;
                case Person.Jobs.musiklehrer : imagesJob1[8].SetActive(true); break;
                case Person.Jobs.fliessbandarbeiter: imagesJob1[9].SetActive(true); break;
            }
        }
        else
        {
            switch (jobname)
            {
                case Person.Jobs.strassenkehrer : imagesJob2[0].SetActive(true); break;
                case Person.Jobs.grafikdesigner : imagesJob2[1].SetActive(true); break;
                case Person.Jobs.bauingenieur : imagesJob2[2].SetActive(true); break;
                case Person.Jobs.farmer : imagesJob2[3].SetActive(true); break;
                case Person.Jobs.gaertner : imagesJob2[4].SetActive(true); break;
                case Person.Jobs.anwalt : imagesJob2[5].SetActive(true); break;
                case Person.Jobs.krankenpfleger : imagesJob2[6].SetActive(true); break;
                case Person.Jobs.politiker : imagesJob2[7].SetActive(true); break;
                case Person.Jobs.kunstlehrer : imagesJob2[8].SetActive(true); break;
                case Person.Jobs.musiklehrer : imagesJob2[8].SetActive(true); break;
                case Person.Jobs.fliessbandarbeiter: imagesJob2[9].SetActive(true); break;
            }
        }
    }

    public void CloseMonitor()
    {
        monitorUI.gameObject.SetActive(false);
    }
    
    public Camera GetCurrentCamera()
    {
        switch (currentState)
        {
            case state.desk:
                return deskCamera;
                break;
            case state.folder:
                return folderCamera;
                break;
            case state.rulesheet:
                return rulesheetCamera;
                break;
        }
        return Camera.current;
    }
    
    private void GetAktenFromFolderGameObjects()
    {
        foreach (var currAkte in foldersGameobjects)
        {
            Akte tmpakte = new Akte();
            akten.Add(tmpakte);
        }
    }
    
    public void CloseCurrentAction()
    {
        if (currentState == state.folder)
        {
            openedFolderOnDesk.SetActive(false);
            closedFolderOnDesk.SetActive(true);
            GetCurrentFolderGameObjectToPerson().SetActive(false);
            deskCamera.gameObject.SetActive(true);
            folderCamera.gameObject.SetActive(false);
            //folderOpened = false; //todo
        }

        if (currentState == state.monitor)
        {
            deskCamera.gameObject.SetActive(true);
        }

        if (currentState == state.rulesheet)
        {
            rulesheetui.SetActive(false);
            rulesheetCamera.gameObject.SetActive(false);
            deskCamera.gameObject.SetActive(true);
        }
        currentState = state.desk;
    }

    private GameObject GetCurrentFolderGameObjectToPerson()
    {
        string name = toworkon.GetName().ToLower();
        foreach (var fld in foldersGameobjects)
        {
            if (fld.name.ToLower().Contains(name)) return fld;
        }
        return null;
    }

    public void SetFirstPageActive()
    {
        string name = toworkon.GetName().ToLower();
        foreach (var fp in firstPages)
        {
            if (fp.name.ToLower().Contains(name)) fp.gameObject.SetActive(true);
        }
    }
    
    public void SetFirstPageInactive()
    {
        string name = toworkon.GetName().ToLower();
        foreach (var fp in firstPages)
        {
            if (fp.name.ToLower().Contains(name)) fp.gameObject.SetActive(false);
        }
    }

    #region gettersetter

    public Person GetCurrentPerson()
    {
        return toworkon;
    }

    public bool HasRuleSheet()
    {
        return hasRulesheet;
    }

    public void SetHasRuleSheet(bool hasrulesheet)
    {
        hasRulesheet = hasrulesheet;
    }
    #endregion
}
