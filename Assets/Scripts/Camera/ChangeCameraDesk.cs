using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraDesk : MonoBehaviour
{
    [SerializeField] private Camera disableCamera;
    [SerializeField] private Camera enableCamera;

    private void Change()
    {
        disableCamera.gameObject.SetActive(false);
        enableCamera.gameObject.SetActive(true);
    }

    private void ChangeBack()
    {
        disableCamera.gameObject.SetActive(true);
        enableCamera.gameObject.SetActive(false);
    }
}
