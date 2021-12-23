using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Parenting
{
    public class Education : MonoBehaviour
    {
        public PopUpManager popUpManager;
        public PopUp educationPopup;

        private void Awake()
        {
        }
        
        private void Update()
        {
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("education is clicked");
            popUpManager.ColliderClickAction(educationPopup);
            Time.timeScale = 0f;
        }
    }
}