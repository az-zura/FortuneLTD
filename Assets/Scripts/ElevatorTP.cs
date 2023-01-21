using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTP : MonoBehaviour
{
    [SerializeField] private bool up;
    [SerializeField] private GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        CharacterController playerController = player.GetComponent<CharacterController>();
        Vector3 movement;
        if (up)
        {
            movement = new Vector3(0, 35.5f, 3);
        }
        else
        {
            movement = new Vector3(0, -34, 5);

        }
        playerController.Move(movement);
    }
}
