using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoadTrigger : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        playerMovement.IncrementRoadTriggerCount();
    }

    private void OnTriggerExit(Collider other)
    {
        playerMovement.DecrementRoadTriggerCount();
    }
}
