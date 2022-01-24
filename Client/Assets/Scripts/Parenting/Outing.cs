using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Baby;
using Controller;

namespace Parenting
{
    public class Outing : MonoBehaviour
    {
        public LoadingBaby loadingBaby;
        public MotiveController motiveController;
                
        private void Start()
        {
            StartCoroutine(InitCoroutine());
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
            if (motiveController.IsEnergyLack())
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

        private IEnumerator InitCoroutine()
        {
            yield return new WaitUntil
            (
                () => loadingBaby.babyObject != null
            );

            var baby = loadingBaby.babyObject;
            Dictionary<string, Decimal> temperament = 
                baby.GetBaby().Temperament;

            babyActivity = Decimal.ToDouble(temperament["activity"]);
            outingProbability = 
                new double[]{ babyActivity, 1.0f - babyActivity };
        }
    }
}