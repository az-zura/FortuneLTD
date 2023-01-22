using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeskManager : MonoBehaviour
{
    private enum state
    {
        desk, folder, monitor, rulesheet
    }

    private state currentState;

    [SerializeField] private MiniGameLoop _miniGameLoop;
    

    #region Cameras
    [SerializeField] private Camera deskCamera;
    [SerializeField] private Camera folderCamera;
    [SerializeField] private Camera rulesheetCamera;
    #endregion

    #region GameObjects on Desk
    [SerializeField] private GameObject folderTray;
    //akten
    [SerializeField] private GameObject folder0;
    [SerializeField] private GameObject folder1;
    [SerializeField] private GameObject folder2;
    [SerializeField] private GameObject folder3;
    [SerializeField] private GameObject folder4;
    [SerializeField] private GameObject folder5;
    [SerializeField] private GameObject folder6;
    [SerializeField] private GameObject folder7;
    [SerializeField] private GameObject folder8;
    [SerializeField] private GameObject folder9;
    [SerializeField] private GameObject folder10;
    [SerializeField] private GameObject folder11;
    [SerializeField] private GameObject folder12;
    [SerializeField] private GameObject folder13;
    [SerializeField] private GameObject folder14;
    [SerializeField] private GameObject folder15;
    [SerializeField] private GameObject folder16;
    [SerializeField] private GameObject folder17;
    [SerializeField] private GameObject folder18;
    [SerializeField] private GameObject folder19;

    [SerializeField] private GameObject _backgroundAkte; //Umschlag ohne Papierinhalt
    [SerializeField] private GameObject closedFolder; 
    [SerializeField] private GameObject folderOnDesk; 

    #endregion

    #region every variable only folder related
    
        //private List<GameObject> foldersGameobjects;
        private List<Akte> akten;

        
        private GameObject currentFolder;
        private Akte currentAkte;
        
        
        private bool folderToWorkOn; //true if there is currently a folder to work on on the desk
        private bool foldersEmpty; //true if there is no new folder to work on or joe
        private bool folderOpened;
        private bool secondPageJustOpened;
        
        private int currentFolderToDo = 0;
    #endregion

    #region every variable only monitor related
        [SerializeField] private Canvas monitor;


        private bool computerToUse; //true if folder is done and now computer is to use
        private bool monitorOpened;
        
    #endregion

 
    private void Start()
    {
        #region Bools
        folderToWorkOn = false;
        foldersEmpty = false;
        computerToUse = false;
        folderOpened = false;
        secondPageJustOpened = true;
        monitorOpened = false;
        #endregion

        currentState = state.desk;
        folderOnDesk.SetActive(false);
        akten = new List<Akte>();
    }
    
    public void ObjectHit(string hitObject)
    {

        switch (hitObject)
        {
            case "FolderTray":
            {
                if (!folderOnDesk.activeSelf && !foldersEmpty)
                {
                    folderOnDesk.gameObject.SetActive(true);
                    folderToWorkOn = true;
                }
                break;
            }
            case "FolderUnopened":
            {
                _backgroundAkte.SetActive(true);
                closedFolder.SetActive(false);
                //problem
                //folder0.setActive(true);
                deskCamera.gameObject.SetActive(false);
                folderCamera.gameObject.SetActive(true);
                folderOpened = true;
                currentState = state.folder;
                break;
            }
            case "FirstPage":
            {
                currentAkte.DisableFirstPageAndImage();
                break;
            }
            case "SecondPage":
            {
                if (secondPageJustOpened)
                {
                    currentAkte.DisableSecondPage();
                }
                else
                {
                    currentAkte.EnableFirstPageAndImage();
                }
                secondPageJustOpened = !secondPageJustOpened;
                break;
            }
            case "ThirdPage":
            {
                currentAkte.EnableSecondPage();
                break;
            }
            case "Monitor":
            {
                if (currentState == state.desk)
                {
                    /*deskCamera.gameObject.SetActive(false);
                    monitorCamera.gameObject.SetActive(true);
                    currentState = state.monitor;
                    */
                    monitor.gameObject.SetActive(true); //enable monitor UI
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

    public void CloseCurrentAction()
    {
        if (currentState == state.folder)
        {
            _backgroundAkte.SetActive(false);
            closedFolder.SetActive(true);
            currentFolder.SetActive(false);
            deskCamera.gameObject.SetActive(true);
            folderCamera.gameObject.SetActive(false);
            folderOpened = false;
        }

        if (currentState == state.monitor)
        {
            deskCamera.gameObject.SetActive(true);
        }
        currentState = state.desk;
    }
    
    public int GetCurrentState()
    {
        switch (currentState)
        {
            case state.desk: return 0;
            case state.folder: return 1;
            case state.monitor: return 2;
        }

        return -1; //error no state
    }

    #region every function only folder related

    
    /*
    public void FindAkteByName(string name)
    {
        GetAktenFromFolderGameObjects();

        foreach (var akte in akten)
        {
            if (akte.GetName().Contains(name))
            {
                currentAkte = akte;
                return;
            }
        }
    }
        
    
        private void GetAktenFromFolderGameObjects()
        {
            foreach (var currAkte in foldersGameobjects)
            {
                Akte tmpakte = new Akte(currAkte);
                akten.Add(tmpakte);
            }
        }

*/
        

        
    #endregion

    public void EnableFolderGameObject()
    {
        
    }
    
}
