using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Controller;

namespace Parenting
{
    public class Sleeping : MonoBehaviour
    {
        public MotiveController motiveController;

        private void Awake()
        {
        }
        
        private void Update()
        {
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("sleeping is clicked");
            CheckAvailable();
        }

        private void CheckAvailable()
        {
            if (motiveController.IsEnergyLack())
            {
                Sleep();
            }
            else
            {
                Debug.Log("Your baby didn't wanna get sleep");
                // 떼부리는 상태로 들어감
            }
        }

        private void Sleep()
        {
        }
    }
}