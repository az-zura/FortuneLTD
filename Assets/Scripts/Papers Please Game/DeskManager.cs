using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> foldersGameobjects;
    private List<Akte> akten;
    [SerializeField] private GameObject _backgroundAkte;
    [SerializeField] private GameObject closedFolder;

    [SerializeField] private Camera folderCamera;
    [SerializeField] private Camera deskCamera;

    private int currentFolderToDo = 0;
    
    private void Start()
    {
        GetAktenFromFolderGameObjects();
    }

    private void GetAktenFromFolderGameObjects()
    {
        foreach (var currentAkte in foldersGameobjects)
        {
            Akte tmpakte = new Akte(currentAkte);
        }
    }
    
    public void ObjectHit(string hitObject)
    {
        if (hitObject.Equals("FolderUnopened"))
        {        
            _backgroundAkte.SetActive(true);
            foldersGameobjects[currentFolderToDo].SetActive(true);
            closedFolder.SetActive(false);
            
            deskCamera.gameObject.SetActive(false);
            folderCamera.gameObject.SetActive(true);
            //GameObject.Find("AkteHintergrund").SetActive(true);
            //GameObject.Find("FolderUnopened").SetActive(false);
        }
    }
}
