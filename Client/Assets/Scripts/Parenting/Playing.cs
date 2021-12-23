using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Parenting
{
    public class Playing : MonoBehaviour
    {
        public PopUpManager popUpManager;
        public PopUp playingPopup;

        private void Awake()
        {
        }
        
        private void Update()
        {
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("playing is clicked");
            popUpManager.ColliderClickAction(playingPopup);
            Time.timeScale = 0f;            
        }
    }
}