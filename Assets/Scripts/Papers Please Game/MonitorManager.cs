using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;

public class MonitorManager : MonoBehaviour
{
    
    [SerializeField] private GameObject desktop;
    [SerializeField] private GameObject deathxcel;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject firstWindow;
    [SerializeField] private GameObject formWindow;
    [SerializeField] private GameObject thirdWindow;
    [SerializeField] private GameObject doYouWantToCloseMessage;
    [SerializeField] private GameObject BerufButtonUnchecked;
    [SerializeField] private GameObject BerufButtonChecked;


    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshProUGUI error;
    [SerializeField] private TextMeshProUGUI currentAge;
    [SerializeField] private TextMeshProUGUI expectedAge;
    
    [SerializeField] private MiniGameLoop _miniGameLoop;
 

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
            _miniGameLoop.SetFirstPageInactive();
        }

        public void BackToDesktop()
        {
            doYouWantToCloseMessage.SetActive(false);
        }

        public void CloseMonitor()
        {
            doYouWantToCloseMessage.SetActive(false);
            deathxcel.SetActive(false);
            image.SetActive(false);
            //firstWindow.SetActive(false);
            formWindow.SetActive(false);
            thirdWindow.SetActive(false);
            //idText.gameObject.SetActive(false);
            error.gameObject.SetActive(false);
            desktop.SetActive(true);
            _miniGameLoop.CloseMonitor();
            _miniGameLoop.SetFirstPageInactive();
        }

    
        public void OpenDoYouReallyWantToCloseMessage()
        {
            doYouWantToCloseMessage.SetActive(true);
        }
        
        
        public void SwitchToSecondWindow(string ID)
        {
            if (ID.Equals(_miniGameLoop.GetCurrentPerson().GetIdentifikation()))
            {
                formWindow.SetActive(true);
                firstWindow.SetActive(false);
                _miniGameLoop.SetFirstPageActive();
                idText.text += ID;
            }
            else
            {
                error.gameObject.SetActive(true);
            }
            
        }
        
        public void SwitchToThirdWindow()
        {
            formWindow.SetActive(false);
            thirdWindow.SetActive(true);
            _miniGameLoop.SetFirstPageInactive();
            expectedAge.text = _miniGameLoop.GetCurrentPerson().GetErwartetesAlter().ToString();
            currentAge.text = _miniGameLoop.GetCurrentPerson().GetDerzeitigesAlter().ToString();
            
        }

        public void CheckBox()
        {
            BerufButtonUnchecked.SetActive(false);
            BerufButtonChecked.SetActive(true);
        }

        public void UncheckBox()
        {
            BerufButtonUnchecked.SetActive(true);
            BerufButtonChecked.SetActive(false);
        }

        

    #endregion
    
    
}
