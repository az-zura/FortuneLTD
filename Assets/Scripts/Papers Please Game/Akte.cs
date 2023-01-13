using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Akte
{
    private bool isFinished;
    private GameObject parent;
    private GameObject image;
    private GameObject firstPage; //general information
    private GameObject secondPage; //special interests
    private GameObject thirdPage; //confidential

    public Akte(GameObject gameObject)
    {
        parent = gameObject;
        Debug.Log("I am the parent " + parent.name);
        Transform _parent = gameObject.transform;
        foreach (Transform child in _parent)
        {
            switch (child.tag)
            {
                case "Akte Image":
                {
                    image = child.gameObject;
                    Debug.Log("I found " + image.name);
                    break;

                }
                case "Akte FirstPage":
                {
                    firstPage = child.gameObject;
                    Debug.Log("I found " + firstPage.name);
                    break;
                }
            }
         
        }
    }
}
