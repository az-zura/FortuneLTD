using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameTrigger : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private bool startGame;

    private float seconds;
    private float secondsUntilMainSceneLaoded;

    private bool isTriggered;
    private void Start()
    {
        secondsUntilMainSceneLaoded = 5;
        seconds = 0;
        isTriggered = false;
    }

    private void Update()
    {
        if (isTriggered)
        {
            seconds += Time.deltaTime;
        }
        Debug.Log("Second MainMenu " + seconds);
        if (seconds >= secondsUntilMainSceneLaoded)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        if (startGame)
        {
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
        seconds = 0;
        if (startGame)
        {
            text.SetActive(false);
        }
    }
}
