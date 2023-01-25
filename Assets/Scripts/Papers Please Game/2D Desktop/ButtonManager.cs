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

        public void CheckBox()
        {
            _monitorManager.CheckBox();
        }

        public void UncheckBox()
        {
            _monitorManager.UncheckBox();
        }
        
        
        
        
        #endregion
}
