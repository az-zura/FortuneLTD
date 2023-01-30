using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.UI;
using UnityEngine.XR;
using Image = UnityEngine.UI.Image;


public class MiniGameLoop : MonoBehaviour
{
    private List<Person> allPersons;
    private List<Person> dailyPersons;
    private int numberOfPersons;
    private Person toworkon;
    private GameObject currentFolderGameObject;
    private State currentState;
    private List<Akte> akten;
    private List<Person.Jobs> dailyJobs;
    private int currentDay = 0;

    public event EventHandler FileFinished;

    [SerializeField] private GameLoop _gameLoop;
    public enum State
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

        [SerializeField] private GameObject rulesheet;
        [SerializeField] private GameObject rulesheetui;
        [SerializeField] private TextMeshProUGUI job1text;
        [SerializeField] private TextMeshProUGUI job2text;
        [SerializeField] private List<GameObject> imagesJob1;
        [SerializeField] private List<GameObject> imagesJob2;

        [SerializeField] private GameObject decke;

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
        //setup for first time
        currentState = State.desk;
        allPersons = Person.InstantiatePersons();
        
        akten = new List<Akte>();
        dailyJobs = new List<Person.Jobs>();
        dailyPersons = new List<Person>();
        _gameLoop.WorkdayStarted += StartWorkDay;
        _gameLoop.WorkdayEnded += EndWorkday;
        GetAktenFromFolderGameObjects();
    }

    
    #region SetupDay
    private void StartWorkDay(object sender, EventArgs eventArgs)
    {
        folderTrayEmpty = false;
        secondPageJustOpened = true;
        hasRulesheet = false;
        
        //get persons for the day as long as they are not finished yet
        numberOfPersons = Random.Range(4, 7);
        for (int i = 0; i < numberOfPersons; i++)
        {
            dailyPersons.Add(GetRandomPerson());
        }

        toworkon = GetRandomPersonToWorkOn();
        GetDailyJobs();
    }

    
    public void EndWorkday(object sender, EventArgs eventArgs)
    {
        dailyPersons.Clear();
        dailyJobs.Clear();
    }

    //workaround for early exit from minigame
    public void EndWorkdayEarly()
    {
        dailyPersons.Clear();
        dailyJobs.Clear();
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
    
    public Person GetRandomPersonToWorkOn()
    {
        int numberOfPersons = dailyPersons.Count;
        return dailyPersons[Random.Range(0, numberOfPersons - 1)];
    }


    public void FinishPerson(bool kill)
    {
        toworkon.Finish(kill);
        dailyPersons.Remove(toworkon);
        if (dailyPersons.Count > 0)
        {
            toworkon = GetRandomPersonToWorkOn();
        }
        else
        {
            _gameLoop.WorkdayEndedEarly = true;
            folderTrayEmpty = true;
            EndWorkdayEarly();
        }
    }
    #endregion

    public void ExitDesk()
    {
        deskCamera.gameObject.SetActive(false);
        decke.SetActive(false);
        _gameLoop.AtDesk = false;
    }
    
    public void ObjectHit(string objectName)
    {
        AudioManager.instance.PlaySound("Click");
        switch (objectName)
        {
            case "FolderTray":
            {
                if (currentState == State.desk)
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
                if (currentState == State.desk)
                {
                    openedFolderOnDesk.SetActive(true);
                    closedFolderOnDesk.SetActive(false);
                    //problem
                    GetCurrentFolderGameObjectToPerson().SetActive(true);
                    toworkon.GetAkte().InstantiateAkte(GetCurrentFolderGameObjectToPerson());
                    Debug.Log(toworkon.GetAkte().GetName());
                    deskCamera.gameObject.SetActive(false);
                    folderCamera.gameObject.SetActive(true);
                    currentState = State.folder;
                }
                break;
            }
            case "FirstPage":
            {
                if (currentState == State.folder)
                {
                    toworkon.GetAkte().DisableFirstPageAndImage();
                }
                break;
            }
            case "SecondPage":
            {
                if (currentState == State.folder)
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
                if (currentState == State.folder)
                {
                    toworkon.GetAkte().EnableSecondPage();
                }
                break;
            }
            case "Monitor":
            {
                if (currentState == State.desk)
                {
                    monitorUI.gameObject.SetActive(true); //enable monitor UI
                    currentState = State.monitor;
                }
                break;
            }
            case "RuleSheet":
            {
                if (currentState == State.desk)
                {
                    currentState = State.rulesheet;
                    rulesheetui.SetActive(true);
                    job1text.text =  dailyJobs[0].ToString();
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
        currentState = State.desk;
    }

    public void CloseCurrentAction()
    {
        if (currentState == State.folder)
        {
            openedFolderOnDesk.SetActive(false);
            closedFolderOnDesk.SetActive(true);
            GetCurrentFolderGameObjectToPerson().SetActive(false);
            deskCamera.gameObject.SetActive(true);
            folderCamera.gameObject.SetActive(false);
            //folderOpened = false; //todo
        }

        if (currentState == State.monitor)
        {
            deskCamera.gameObject.SetActive(true);
        }

        if (currentState == State.rulesheet)
        {
            rulesheetui.SetActive(false);
            rulesheetCamera.gameObject.SetActive(false);
            deskCamera.gameObject.SetActive(true);
        }
        currentState = State.desk;
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
    
    private void GetAktenFromFolderGameObjects()
    {
        foreach (var currAkte in foldersGameobjects)
        {
            Akte tmpakte = new Akte();
            akten.Add(tmpakte);
        }
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

    public bool InputAbschlussStimmt(int score)
    {
        switch (toworkon.GetAusbildung())
        {
            case Person.Bildungsstand.keinAbschluss:
            {
                if (score == 10) return true;
                break;
            }
            case Person.Bildungsstand.schule:
            {
                if (score == 20) return true;
                break;
            }
            case Person.Bildungsstand.ausbildung:
            {
                if (score == 35) return true;
                break;
            }
            case Person.Bildungsstand.studium:
            {
                if (score == 50) return true;
                break;
            }
        }
        return false;
    }

    public bool InputJobStimmt(int score)
    {
        if ((dailyJobs[0].Equals(toworkon.GetJob()) && score == 30) ||
            (dailyJobs[1].Equals(toworkon.GetJob()) && score == -10) || (!dailyJobs[0].Equals(toworkon.GetJob()) && !dailyJobs[1].Equals(toworkon.GetJob()) && score == 10))
        {
            return true;
        }
        return false;
    }

    public void ActivateRulesheet()
    {
        rulesheet.SetActive(true);
    }

    public int CalculateDailyScore()
    {
        return Random.Range(150, 300);
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

    public MiniGameLoop.State GetCurrentState()
    {
        return currentState;
    }
    
    public Camera GetCurrentCamera()
    {
        switch (currentState)
        {
            case State.desk:
                return deskCamera;
            case State.folder:
                return folderCamera;
            case State.rulesheet:
                return rulesheetCamera;
            case State.monitor:
                return null;
        }
        return Camera.current;
    }
    #endregion
}
