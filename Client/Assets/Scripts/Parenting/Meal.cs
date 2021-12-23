using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Parenting
{
    public class Meal : MonoBehaviour
    {
        public PopUpManager popUpManager;
        public PopUp mealPopup;

        private void Awake()
        {
        }
        
        private void Update()
        {
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("meal is clicked");
            popUpManager.ColliderClickAction(mealPopup);
            Time.timeScale = 0f;            
        }
    }
}