using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private Camera disableCamera;
    [SerializeField] private Camera enableCamera;

    [SerializeField] private bool changeToDesk;
    [SerializeField] private GameObject decke;
    [SerializeField] private GameObject vents;
    private void OnTriggerEnter(Collider other)
    {
        disableCamera.gameObject.SetActive(false);
        enableCamera.gameObject.SetActive(true);
        if(changeToDesk) {
            decke.SetActive(true);
            vents.SetActive(true);
        }
       
    }

    
}
