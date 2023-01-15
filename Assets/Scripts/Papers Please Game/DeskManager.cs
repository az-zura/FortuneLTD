using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    private enum state
    {
        desk, folder, monitor
    }

    private state currentState;
    
    [SerializeField] private GameObject folderTray;
    [SerializeField] private Camera deskCamera;

    #region every variable only folder related
        [SerializeField] private List<GameObject> foldersGameobjects;
        private List<Akte> akten;
        
        [SerializeField] private GameObject _backgroundAkte;
        [SerializeField] private GameObject closedFolder;
        [SerializeField] private GameObject folderOnDesk;
        
        [SerializeField] private Camera folderCamera;
        
        private bool folderToWorkOn; //true if there is currently a folder to work on on the desk
        private bool foldersEmpty; //true if there is no new folder to work on or joe
        private bool folderOpened;
        private bool secondPageJustOpened;
        
        private int currentFolderToDo = 0;
    #endregion

    #region every variable only monitor related
        [SerializeField] private GameObject monitor;

        [SerializeField] private Camera monitorCamera;

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
        GetAktenFromFolderGameObjects();
    }
    
    public void ObjectHit(string hitObject)
    {
        switch (hitObject)
        {
            case "FolderUnopened":
            {
                _backgroundAkte.SetActive(true);
                foldersGameobjects[GetIndexOfActiveAkte()].SetActive(true);
                closedFolder.SetActive(false);
            
                deskCamera.gameObject.SetActive(false);
                folderCamera.gameObject.SetActive(true);
                folderOpened = true;
                currentState = state.folder;
                break;
            }
            case "FolderTray":
            {
                if (!folderOnDesk.activeSelf && !foldersEmpty)
                {
                    folderOnDesk.gameObject.SetActive(true);
                    folderToWorkOn = true;
                }
                break;
            }
            case "FirstPage":
            {
                akten[GetIndexOfActiveAkte()].DisableFirstPageAndImage();
                break;
            }
            case "SecondPage":
            {
                if (secondPageJustOpened)
                {
                    akten[GetIndexOfActiveAkte()].DisableSecondPage();
                }
                else
                {
                    akten[GetIndexOfActiveAkte()].EnableFirstPageAndImage();
                }
                secondPageJustOpened = !secondPageJustOpened;
                break;
            }
            case "ThirdPage":
            {
                akten[GetIndexOfActiveAkte()].EnableSecondPage();
                break;
            }
            case "Monitor":
            {
                if (currentState == state.desk)
                {
                    deskCamera.gameObject.SetActive(false);
                    monitorCamera.gameObject.SetActive(true);
                    currentState = state.monitor;
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
            foldersGameobjects[GetIndexOfActiveAkte()].SetActive(false);
            deskCamera.gameObject.SetActive(true);
            folderCamera.gameObject.SetActive(false);
            folderOpened = false;
        }

        if (currentState == state.monitor)
        {
            deskCamera.gameObject.SetActive(true);
            monitorCamera.gameObject.SetActive(false);
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
        int GetIndexOfActiveAkte()
        {
            foreach (Akte akte in akten)
            {
                if (!akte.isFinished)
                {
                    return akten.IndexOf(akte);
                }
            }
            return -1;
        }
        
        private void GetAktenFromFolderGameObjects()
        {
            foreach (var currentAkte in foldersGameobjects)
            {
                Akte tmpakte = new Akte(currentAkte);
                akten.Add(tmpakte);
            }
        }
    #endregion
}
