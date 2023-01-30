using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameTrigger : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private bool startGame;

    [SerializeField] private List<GameObject> flowers;


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
        //flowers
        if (seconds >= 0.5f)
        {
           flowers[0].SetActive(true);
        }
        if (seconds >= 1.3f)
        {
            flowers[1].SetActive(true);
        }
        if (seconds >= 2)
        {
            flowers[2].SetActive(true);
        }
        if (seconds >= 2.7f)
        {
            flowers[3].SetActive(true);
        }
        if (seconds >= 3.4f)
        {
            flowers[4].SetActive(true);
        }
        if (seconds >= 4.2f)
        {
            flowers[5].SetActive(true);
        }
       
    }

    private void DeactivateAllFlowers()
    {
        foreach (GameObject flower in flowers)
        {
            flower.SetActive(false);
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
        DeactivateAllFlowers();
        if (startGame)
        {
            text.SetActive(false);
        }
    }
}
