using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingNamePopup : MonoBehaviour, IPopUp
    {
        public void OpenPopUp()
        {
            this.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        public void ClosePopUp()
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f;            
        }
    }
}