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
            movement = new Vector3(0, 36, 3);
        }
        else
        {
            movement = new Vector3(0, -36f, 5);

        }
        playerController.Move(movement);
    }
}
