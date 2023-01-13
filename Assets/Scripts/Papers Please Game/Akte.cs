using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Akte
{
    public bool isFinished;
    private GameObject parent;
    private GameObject image;
    private GameObject firstPage; //general information
    private GameObject secondPage; //special interests
    private GameObject thirdPage; //confidential

    public Akte(GameObject gameObject)
    {
        parent = gameObject;
        isFinished = false;
        Transform _parent = gameObject.transform;
        foreach (Transform child in _parent)
        {
            switch (child.tag)
            {
                case "Akte Image":
                {
                    image = child.gameObject;
                    break;

                }
                case "Akte FirstPage":
                {
                    firstPage = child.gameObject;
                    break;
                }
                case "Akte SecondPage":
                {
                    secondPage = child.gameObject;
                    break;
                }
                case "Akte ThirdPage":
                {
                    thirdPage = child.gameObject;
                    break;
                }

            }
        }
    }

    public void OpenSecondPage()
    {
        firstPage.SetActive(false);
    }

    public void OpenThirdPage()
    {
        secondPage.SetActive(false);
    }
    
}
