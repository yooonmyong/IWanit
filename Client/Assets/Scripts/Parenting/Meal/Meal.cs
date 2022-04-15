using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Controller;

namespace Parenting
{
    public class Meal : MonoBehaviour
    {
        public MealPopup mealPopup;
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
            mealPopup.OpenPopUp();
        }

        public void CheckAvailable(FoodObject foodObject)
        {
            if (motiveController.IsHungerLack())
            {
                // 식사 시작
            }
            else
            {
                Debug.Log("Your baby didn't wanna have a meal");
                // 떼부리는 상태로 들어감
            }
        }
    }
}