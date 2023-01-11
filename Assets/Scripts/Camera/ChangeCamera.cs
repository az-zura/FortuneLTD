using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private Camera disableCamera;
    [SerializeField] private Camera enableCamera;

    private void OnTriggerEnter(Collider other)
    {
        disableCamera.gameObject.SetActive(false);
        enableCamera.gameObject.SetActive(true);
    }
}
