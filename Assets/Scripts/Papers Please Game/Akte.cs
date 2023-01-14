using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Akte
{
    public bool isFinished;
    private GameObject parent;
    private GameObject image;
    private GameObject firstPage; //general information
    private GameObject secondPage; //special interests
    private GameObject thirdPage; //confidential
    private bool secondPageOpened = false; //first page and image are on the left side
    private bool thirdPageOpened = false; //second page is on the left
    
    public Akte(GameObject gameObject)
    {
        parent = gameObject;
        isFinished = false;
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
    }

    public void OpenOrCloseSecondPage()
    {
        if (!secondPageOpened)
        {
            firstPage.transform.Translate(new Vector3(0.55f, 0,0));
            image.transform.Translate(new Vector3(0.55f, 0,0.1f));
            secondPageOpened = true;
        }
        else
        {
            firstPage.transform.Translate(new Vector3(-0.55f, 0,0));
            image.transform.Translate(new Vector3(-0.55f, 0,-0.1f));
            secondPageOpened = false;
        }
        
    }

    public void OpenOrCloseThirdPage()
    {
        if (!thirdPageOpened)
        {
             secondPage.transform.Translate(new Vector3(0.55f, 0.03f,0));
             thirdPageOpened = true;
        }
        else
        {
            Debug.Log("BACK TO SECOND PAGE");
            secondPage.transform.Translate(new Vector3(-0.55f, -0.03f, 0));
            thirdPageOpened = false;
        }
    }

    public void FinishAkte()
    {
        isFinished = true;
    }
}
