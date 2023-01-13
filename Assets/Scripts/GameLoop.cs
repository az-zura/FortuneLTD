using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private int currentDay;

    private void Start()
    {
        currentDay = 0;
        
    }

    private void NextDay()
    {
        currentDay++;
    }
}
