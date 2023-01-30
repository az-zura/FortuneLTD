using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private MonitorManager _monitorManager;

    #region Navigation
        public void OpenFamilyPic()
        {
            AudioManager.instance.PlaySound("Click");
            _monitorManager.OpenImage();
        }

        public void CloseFamilyPic()
        {
            AudioManager.instance.PlaySound("Click");
            _monitorManager.CloseImage();
        }

        public void OpenDeathxcel()
        {
            AudioManager.instance.PlaySound("Click");
            _monitorManager.OpenDeathxcel();
        }

        public void CloseDeathxcel()
        {
            AudioManager.instance.PlaySound("Click");
            _monitorManager.CloseDeathxcel();
        }

        public void AddPersonToDeathxcel(TMP_InputField idInput)
        {
            if(idInput.text != string.Empty) _monitorManager.SwitchToSecondWindow(idInput.text);
        }

        public void DoYouReallyWantToCloseMonitor()
        { 
            AudioManager.instance.PlaySound("Click");
            _monitorManager.OpenDoYouReallyWantToCloseMessage();
        }

        public void CloseMonitor()
        {
            AudioManager.instance.PlaySound("Click");
            _monitorManager.CloseMonitor();
        }

        public void BackToDesktop()
        {
            AudioManager.instance.PlaySound("Click");
            _monitorManager.BackToDesktop();
        }

        public void DropDownJobsClicked()
        {
            _monitorManager.ActivateDropDownJobs();
        }

        public void DropDownAbschlussClicked()
        {
            _monitorManager.ActivateDropDownAbschluss();
        }

        public void JobClicked()
        {
            _monitorManager.DeactivateDropDownJobs();
        }
        public void AbschlussClicked()
        {
            _monitorManager.DeactivateDropDownAbschluss();
        }
        public void JobGesucht()
        {
            _monitorManager.JobGesucht();
        }
        public void JobNeutral()
        {
            _monitorManager.JobNeutral();
        }
        public void JobUeberschuessig()
        {
            _monitorManager.JobUeberschuessig();
        }
        public void AbschlussKeiner()
        {
            _monitorManager.AbschlussKeiner();
        }
        public void AbschlussSchule()
        {
            _monitorManager.AbschlussSchule();
        }
        public void AbschlussAusbildung()
        {
            _monitorManager.AbschlussAusbildung();
        }
        public void AbschlussStudium()
        {
            _monitorManager.AbschlussStudium();
        }
        public void CalculateScore()
        {
            _monitorManager.CalculateScore();
            _monitorManager.SwitchToScoreWindow();
        }
        public void Umbringen()
        {
            _monitorManager.FinishPerson(true);
        }
        public void LebenLassen()
        {
            _monitorManager.FinishPerson(false);
        }

        #endregion
}
