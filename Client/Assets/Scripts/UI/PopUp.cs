using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PopUp : MonoBehaviour
    {
        public GameObject ClosingButton;

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