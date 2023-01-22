using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Akte
{
    public bool isConfidential;
    private GameObject parent;
    private GameObject image;
    private GameObject firstPage; //general information
    private GameObject secondPage; //special interests
    private GameObject thirdPage; //confidential
    private bool secondPageOpened; //first page and image are on the left side
    private bool thirdPageOpened; //second page is on the left

    private string name;

    public Akte(GameObject gameObject)
    {
        parent = gameObject;
        secondPageOpened = false; 
        thirdPageOpened = false;
        isConfidential = true;
        Transform _parent = gameObject.transform;
        foreach (Transform child in _parent)
        {
            switch (child.name)
            {
                case "Image":
                {
                    image = child.gameObject;
                    break;

                }
                case "FirstPage":
                {
                    firstPage = child.gameObject;
                    break;
                }
                case "SecondPage":
                {
                    secondPage = child.gameObject;
                    break;
                }
                case "ThirdPage":
                {
                    thirdPage = child.gameObject;
                    break;
                }

            }
        }

        name = parent.name.ToLower();
    }

    public void DisableFirstPageAndImage()
    {
        firstPage.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
    }

    public void EnableFirstPageAndImage()
    {
        firstPage.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
    }

    public void DisableSecondPage()
    {
        if (!isConfidential)
        {
            secondPage.gameObject.SetActive(false);
        }
    }

    public void EnableSecondPage()
    {
        if (!isConfidential)
        {
            secondPage.gameObject.SetActive(true);
        }
    }

    public string GetName()
    {
        return name;
    }
    
}
