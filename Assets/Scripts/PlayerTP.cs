using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTP : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform teleportPoint;
    private void OnTriggerEnter(Collider other)
    {
        CharacterController playerController = player.GetComponent<CharacterController>();

        //playerController.enabled = false;
        //player.transform.position = teleportPoint.transform.position;
        //playerController.enabled = true;

        playerController.Move(teleportPoint.transform.position - player.transform.position);

    }
}
