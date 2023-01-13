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
    
    private bool folderOpened; //true if there is currently a folder to work on on the desk
    private bool foldersEmpty; //true if there is no new folder to work on or joe
    private bool computerToUse; //true if folder is done and now computer is to use
    
    
    
    
    private int currentFolderToDo = 0;
    
    private void Start()
    {
        #region Bools
        folderOpened = false;
        foldersEmpty = false;
        computerToUse = false;

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
        Debug.Log(hitObject);
        switch (hitObject)
        {
            case "FolderUnopened":
            {
                _backgroundAkte.SetActive(true);
                foldersGameobjects[GetIndexOfActiveAkte()].SetActive(true);
                closedFolder.SetActive(false);
            
                deskCamera.gameObject.SetActive(false);
                folderCamera.gameObject.SetActive(true);
                break;
            }
            case "FolderTray":
            {
                if (!folderOnDesk.activeSelf && !foldersEmpty)
                {
                    folderOnDesk.gameObject.SetActive(true);
                    folderOpened = true;
                }
                break;
            }
            case "FirstPage":
            {
                Debug.Log("Works");
                akten[GetIndexOfActiveAkte()].OpenSecondPage();
                break;
            }
            case "SecondPage":
            {
                akten[GetIndexOfActiveAkte()].OpenThirdPage();
                break;
            }
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
