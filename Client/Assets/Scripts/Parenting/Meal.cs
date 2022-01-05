using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Controller;

namespace Parenting
{
    public class Meal : MonoBehaviour
    {
        public PopUpManager popUpManager;
        public PopUp mealPopup;
        public MotiveController motiveController;

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

        public void CheckAvailable(FoodObject foodObject)
        {
            if (motiveController.DoesHungerLack())
            {
            }
            else
            {
                Debug.Log("Your baby didn't wanna have a meal");
            }
        }
    }
}