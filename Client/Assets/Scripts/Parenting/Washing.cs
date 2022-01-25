using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Baby;
using Controller;
using Module;
using UI;

namespace Parenting
{
    public class Washing : MonoBehaviour
    {
        public Animator steeringWheelAnimator;
        public Animator thermometerAnimator;
        public Animator toolsAnimator;
        public LoadingBaby loadingBaby;
        public MotiveController motiveController;
        public GameObject thermometer;
        public Image background;
        public Toast toastPopup;
        private AnimationClip currentAnimatorClip;
        private AnimatorStateInfo currentAnimatorStateInfo;
        private Animator babyAnimator;

        private void Start()
        {
            StartCoroutine(InitCoroutine());
        }

        private void Update()
        {
            StartCoroutine(CheckStateCoroutine());
        }

        private void OnMouseUpAsButton()
        {
            if (motiveController.IsStressFull())
            {
                Debug.Log("Baby is under too much stress!");
            }
            else
            {
                background.sprite = 
                    Resources.Load<Sprite>("Sprites/Background/Bathroom");
                PrepareWashing();
            }
        }

        public void CheckTemperature()
        {
            thermometerAnimator.enabled = false;
            currentAnimatorClip = 
                thermometerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;
            currentAnimatorStateInfo =
                thermometerAnimator.GetCurrentAnimatorStateInfo(0);
            var currentTemperature = 
                (int)(
                    currentAnimatorClip.length * 
                    (currentAnimatorStateInfo.normalizedTime % 1) * 
                    currentAnimatorClip.frameRate
                );
            
            if (Constants.ProperTemperatures.Contains(currentTemperature))
            {
                var color = 
                    new Color
                    (
                        babyImage.color.r, 
                        babyImage.color.g, 
                        babyImage.color.b, 
                        1f
                    );

                babyAnimator.SetBool("wash", true);
                thermometer.GetComponent<SmallerAnimation>().isDisappear = true;
                babyImage.color = color;
                toolsAnimator.SetBool("hidden", false);
            }
            else
            {
                toastPopup.Appear();
                thermometerAnimator.enabled = true;
            }
        }
        }

        private IEnumerator InitCoroutine()
        {
            yield return new WaitUntil
            (
                () => loadingBaby.babyObject != null
            );
        private void PrepareWashing()
        {
            var color = 
                new Color
                (
                    babyImage.color.r, 
                    babyImage.color.g, 
                    babyImage.color.b, 
                    0f
                );

            babyImage.color = color;
            steeringWheelAnimator.SetBool("hidden", true);
            thermometer.SetActive(true);
        }

        }
    }
}