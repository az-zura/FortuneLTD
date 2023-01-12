using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> folders;
    [SerializeField] private GameObject _backgroundAkte;
    [SerializeField] private GameObject closedFolder;

    [SerializeField] private Camera folderCamera;
    [SerializeField] private Camera deskCamera;
    public void ObjectHit(string hitObject)
    {
        if (hitObject.Equals("FolderUnopened"))
        {        
            _backgroundAkte.SetActive(true);
            folders[0].SetActive(true);
            closedFolder.SetActive(false);
            
            deskCamera.gameObject.SetActive(false);
            folderCamera.gameObject.SetActive(true);
            //GameObject.Find("AkteHintergrund").SetActive(true);
            //GameObject.Find("FolderUnopened").SetActive(false);
        }
    }
}
