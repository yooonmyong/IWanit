using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace Parenting
{
    public class Washing : MonoBehaviour
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
            Debug.Log("washing is clicked");
            CheckAvailable();
        }

        private void CheckAvailable()
        {
        }

        private void Wash()
        {
        }
    }
}