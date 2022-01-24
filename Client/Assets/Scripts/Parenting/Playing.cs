using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Controller;

namespace Parenting
{
    public class Playing : MonoBehaviour
    {
        public PlayingPopup playingPopup;
        public MotiveController motiveController;

        private void Awake()
        {
        }
        
        private void Update()
        {
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("playing is clicked");
            playingPopup.OpenPopUp();
        }

        public void CheckAvailable(PlayingObject playingObject)
        {
            if (motiveController.IsEnergyLack())
            {
                Debug.Log("Baby doesn't have energy much as to play!");                
            }
            else
            {
            }
        }
    }
}