using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Baby;
using Module;

namespace Parenting
{
    public class Toilet : MonoBehaviour
    {
        public LoadingBaby loadingBaby;
        public Toast toastPopup;
        private bool isAvailable;
        private float timer;
        private float fullTime;
        private uint babyMonths;

        private void Start()
        {
            StartCoroutine(InitCoroutine());
        }
        
        private void Update()
        {
            timer += Time.deltaTime;
            if (timer == fullTime)
            {
                timer = 0.0f;
                isAvailable = true;
            }
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("toilet is clicked");
            CheckAvailable();
        }

        private void CheckAvailable()
        {
            if (!isAvailable)
            {
                Debug.Log("Baby has just peed!");
                toastPopup.Appear();
            }
            else
            {
                isAvailable = false;
                Pee();
            }
        }

        private IEnumerator InitCoroutine()
        {
            yield return new WaitUntil
            (
                () => loadingBaby.babyObject != null
            );
            
            babyMonths = loadingBaby.babyObject.GetBaby().Months;
            isAvailable = true;
            timer = 0.0f;
            fullTime = 300.0f;
        }

        private void Pee()
        {
            if (babyMonths == Constants.MonthsofPottyTraining)
            {
            }
            else
            {
            }
        }
    }
}