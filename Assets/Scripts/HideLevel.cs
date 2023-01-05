using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLevel : MonoBehaviour
{
    public GameObject[] hideOnEnter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var obj in hideOnEnter)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var obj in hideOnEnter)
        {
            obj.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
