using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DeskManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> foldersGameobjects;
    private List<Akte> akten;
    [SerializeField] private GameObject _backgroundAkte;
    [SerializeField] private GameObject folderTray;
    [SerializeField] private GameObject closedFolder;
    [SerializeField] private GameObject folderOnDesk;

    [SerializeField] private Camera folderCamera;
    [SerializeField] private Camera deskCamera;
    [SerializeField] private Camera monitorCamera;
    
    private bool folderToWorkOn; //true if there is currently a folder to work on on the desk
    private bool foldersEmpty; //true if there is no new folder to work on or joe
    private bool computerToUse; //true if folder is done and now computer is to use
    private bool folderOpened;
    
    
    
    private int currentFolderToDo = 0;
    
    private void Start()
    {
        #region Bools
        folderToWorkOn = false;
        foldersEmpty = false;
        computerToUse = false;
        folderOpened = false;
        #endregion
        
        folderOnDesk.SetActive(false);
        akten = new List<Akte>();
        GetAktenFromFolderGameObjects();
    }

    private void GetAktenFromFolderGameObjects()
    {
        foreach (var currentAkte in foldersGameobjects)
        {
            Akte tmpakte = new Akte(currentAkte);
            akten.Add(tmpakte);
        }
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
                akten[GetIndexOfActiveAkte()].OpenOrCloseSecondPage();
                break;
            }
            case "SecondPage":
            {
                akten[GetIndexOfActiveAkte()].OpenOrCloseThirdPage();
                break;
            }
            case "Monitor":
            {
                deskCamera.gameObject.SetActive(false);
                monitorCamera.gameObject.SetActive(true);
                break;
            }
        }
       
    }

    public void CloseCurrentAction()
    {
        if (folderOpened)
        {
            _backgroundAkte.SetActive(false);
            closedFolder.SetActive(true);
            foldersGameobjects[GetIndexOfActiveAkte()].SetActive(false);
            deskCamera.gameObject.SetActive(true);
            folderCamera.gameObject.SetActive(false);
            folderOpened = false;
        }
    }

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
}
