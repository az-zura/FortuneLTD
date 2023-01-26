using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventTrigger : MonoBehaviour
{
    public string soundName = "";
    public bool onThisGO = true;
    public bool stopAfterExit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            AudioManager.instance.PlaySound(soundName, onThisGO ? gameObject : null);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (stopAfterExit)
            {
                AudioManager.instance.StopSound(soundName, onThisGO ? gameObject : null);
            }
        }
    }
}
