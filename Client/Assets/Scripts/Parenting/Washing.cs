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
        public List<Brushing> brushingTools;
        public Camera camera;
        public LoadingBaby loadingBaby;
        public MotiveController motiveController;
        public GameObject thermometer;
        public GameObject gauze;
        public GameObject toothbrush;
        public Image background;
        public Toast toastPopup;
        private AnimationClip currentAnimatorClip;
        private AnimatorStateInfo currentAnimatorStateInfo;
        private Animator babyAnimator;
        private float defaultOrthographicSize;
        private Image babyImage;
        private Image toothbrushImage;

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

        public void PrepareBrushingTeeth()
        {
            babyAnimator.SetBool("brush", true);
            camera.orthographicSize = Constants.ZoomIn;
            loadingBaby.babyPrefab.transform.localPosition = 
                Constants.BrushingTeethTransform;
            gauze.SetActive(true);
            brushingTools.ForEach(brushingTool => brushingTool.enabled = true);
        }
        private IEnumerator CheckStateCoroutine()
        {
            yield return new WaitUntil
            (
                () => brushingTools.All(brushingTool => brushingTool != null)
            );
            
            if 
            (
                brushingTools.All
                (
                    brushingTool => brushingTool.enabled && brushingTool.isDone
                )
            )
            {
                UnloadBrushingTeeth();
            }
        }

        private IEnumerator InitCoroutine()
        {
            yield return new WaitUntil
            (
                () => loadingBaby.babyObject != null
            );

            babyImage = loadingBaby.babyPrefab.gameObject.GetComponent<Image>();
            babyAnimator = 
                loadingBaby.babyPrefab.gameObject.GetComponent<Animator>();
            defaultOrthographicSize = camera.orthographicSize;
            toothbrushImage = toothbrush.GetComponent<Image>();
        }

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

        private void UnloadBrushingTeeth()
        {
            brushingTools.ForEach(brushingTool => brushingTool.enabled = false);
            gauze.SetActive(false);
            loadingBaby.babyPrefab.transform.localPosition = 
                Constants.InitializedTransform;
            camera.orthographicSize = defaultOrthographicSize;
            babyAnimator.SetBool("brush", false);
            toothbrush.GetComponent<Button>().enabled = false;
            toothbrushImage.sprite =
                Resources.Load<Sprite>
                (
                    "Sprites/Washing/" + toothbrushImage.sprite.name + "_gray"
                );
            var cleanliness = 
                Random.Range
                (
                    (float)Constants.MinimalCleanliness, 
                    (float)Constants.FullMotive
                );

            motiveController.UpdateHygiene((double)cleanliness);
            soapImage.sprite = enabledSoapImage; 
            soap.GetComponent<Button>().enabled = true;
        }
        }
    }
}