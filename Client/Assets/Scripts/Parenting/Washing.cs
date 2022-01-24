using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace Parenting
{
    public class Washing : MonoBehaviour
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
            Debug.Log("washing is clicked");
            CheckAvailable();
        }

        private void CheckAvailable()
        {
        }

        private void Wash()
        private IEnumerator InitCoroutine()
        {
            yield return new WaitUntil
            (
                () => loadingBaby.babyObject != null
            );
        }
    }
}