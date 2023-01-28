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

    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshProUGUI error;

    [SerializeField] private TMP_InputField years;
    //Dropdown jobs
    [SerializeField] private GameObject jobGesucht;
    [SerializeField] private GameObject jobNeutral;
    [SerializeField] private GameObject jobUeberschuessig;
    [SerializeField] private TextMeshProUGUI jobText;

    //Dropdown Abschluss
    [SerializeField] private GameObject keinAbschluss;
    [SerializeField] private GameObject schulabschluss;
    [SerializeField] private GameObject ausbildung;
    [SerializeField] private GameObject studium;
    [SerializeField] private TextMeshProUGUI abschlussText;

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
                thirdWindow.SetActive(true);
                firstWindow.SetActive(false);
                //_miniGameLoop.SetFirstPageActive();
                //idText.text += ID;
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
        }

        public void ActivateDropDownJobs()
        {
            jobGesucht.SetActive(true);
            jobNeutral.SetActive(true);
            jobUeberschuessig.SetActive(true);
        }

        public void ActivateDropDownAbschluss()
        {
            keinAbschluss.SetActive(true);
            schulabschluss.SetActive(true);
            ausbildung.SetActive(true);
            studium.SetActive(true);
        }
        
        public void DeactivateDropDownJobs()
        {
            jobGesucht.SetActive(false);
            jobNeutral.SetActive(false);
            jobUeberschuessig.SetActive(false);
        }

        public void DeactivateDropDownAbschluss()
        {
            keinAbschluss.SetActive(false);
            schulabschluss.SetActive(false);
            ausbildung.SetActive(false);
            studium.SetActive(false);
        }

        public void JobGesucht()
        {
            jobText.text = "30";
        }

        public void JobNeutral()
        {
            jobText.text = "10";

        }

        public void JobUeberschuessig()
        {
            jobText.text = "-10";

        }

        public void AbschlussKeiner()
        {
            abschlussText.text = "10";
        }
        public void AbschlussSchule()
        {
            abschlussText.text = "20";
        }
        public void AbschlussAusbildung()
        {
            abschlussText.text = "35";
        }
        public void AbschlussStudium()
        {
            abschlussText.text = "50";
        }

        public void CalculateScore()
        {
            _miniGameLoop.CalculateScore(Int32.Parse(years.text), jobText.text, abschlussText.text);
        }

    #endregion
    
    
}
