using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonitorManager : MonoBehaviour
{
    
    [SerializeField] private GameObject desktop;
    [SerializeField] private GameObject deathxcel;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject firstWindow;
    [SerializeField] private GameObject formWindow;
    [SerializeField] private GameObject calculatorWindow;

    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshProUGUI error;

    [SerializeField] private MiniGameLoop _miniGameLoop;
    
    //mouse click sounds
    private void Start()
    {
    }

    #region Navigation
    public void OpenImage()
        {
            desktop.SetActive(false);
            image.SetActive(true);
        }
        public void CloseImage()
        {
            image.SetActive(false);
            desktop.SetActive(true);
        }
        public void OpenDeathxcel()
        {
            deathxcel.SetActive(true);
            desktop.SetActive(false);
        }
        public void CloseDeathxcel()
        {
            deathxcel.SetActive(false);
            desktop.SetActive(true);
        }
        
        public void SwitchToSecondWindow(string ID)
        {
            if (ID.Equals(_miniGameLoop.GetCurrentPerson().GetIdentifikation()))
            {
                formWindow.SetActive(true);
                firstWindow.SetActive(false);
            }
            else
            {
                error.gameObject.SetActive(true);
            }
        }
        
        public void SwitchToThirdWindow()
        {
            formWindow.SetActive(false);
            calculatorWindow.SetActive(true);
            Debug.Log(_miniGameLoop.GetCurrentPerson().GetName());
        }

        

    #endregion
    
    
}
