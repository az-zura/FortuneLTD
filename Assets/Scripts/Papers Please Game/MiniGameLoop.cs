using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniGameLoop : MonoBehaviour
{
    private List<Person> allPersons;
    private Person toworkon;
    private GameObject currentFolderGameObject;
    private state currentState;
    private List<Akte> akten;

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
    #endregion

    #region Cameras
        [SerializeField] private Camera deskCamera;
        [SerializeField] private Camera folderCamera;
        [SerializeField] private Camera rulesheetCamera;
        #endregion

    #region  bools

        private bool secondPageJustOpened;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentState = state.desk;
        allPersons = Person.InstantiatePersons();
        folderTrayEmpty = false;
        secondPageJustOpened = true;
        akten = new List<Akte>();
        GetAktenFromFolderGameObjects();
        StartWorkDay();
    }
    

    private void UpdateDay()
    {
        currentDay++;
    }

    private void StartWorkDay()
    {
        toworkon = getRandomPerson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Person getRandomPerson()
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
                    if (secondPageJustOpened)
                    {
                        toworkon.GetAkte().DisableSecondPage();
                    }
                    else
                    {
                        toworkon.GetAkte().EnableFirstPageAndImage();
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
                }
                break;
            }
    
        }
    }

    public Camera GetCurrentCamera()
    {
        switch (currentState)
        {
            case state.desk:
                return deskCamera;
                Debug.Log("Yes");
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
    
}
