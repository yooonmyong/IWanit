using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Controller;

namespace Parenting
{
    public class Outing : MonoBehaviour
    {
        private void Awake()
        public MotiveController motiveController;
        {
        }
        
        private void Update()
        {
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("outing is clicked");
            CheckAvailable();
        }

        private void CheckAvailable()
        {
            if (motiveController.DoesEnergyLack())
            {
                Debug.Log("Baby doesn't have energy much as to go out!");
            }
            else
            {
            }
        }

        private void GoOut()
        {
        }
    }
}