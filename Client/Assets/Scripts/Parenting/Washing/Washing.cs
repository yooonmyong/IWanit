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
        public GameObject lotion;
        public GameObject soap;
        public GameObject showercap;
        public GameObject showerhead;
        public Image background;
        public TimeController timeController;
        public Toast toastPopup;
        private AnimationClip currentAnimatorClip;
        private AnimatorStateInfo currentAnimatorStateInfo;
        private Animator babyAnimator;
        private float defaultOrthographicSize;
        private Image babyImage;
        private Image lotionImage;
        private Image showerheadImage;
        private Image soapImage;
        private Image toothbrushImage;
        private PuttingLotion puttingLotion;
        private Rinsing rinsing;
        private Soaping soaping;
        private Sprite enabledLotionImage;
        private Sprite enabledShowerheadImage;
        private Sprite enabledSoapImage;
        private string currentTime;

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
                babyAnimator.SetBool("wash", true);
                thermometer.GetComponent<SmallerAnimation>().isDisappear = true;
                toolsAnimator.SetBool("hidden", false);
                HideBaby(false);
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

        public void PreparePuttingLotion()
        {
            if (timeController.IsNight())
            {
                currentTime = "night";
            }
            else
            {
                currentTime = "day";
            }

            background.sprite = 
                Resources.Load<Sprite>("Sprites/Background/Idle_" + currentTime);
            babyAnimator.SetBool("cap", false);
            babyAnimator.SetBool("wash", false);
            babyAnimator.SetBool("lotion", true);
            lotion.GetComponent<PuttingLotion>().enabled = true;
        }

        public void PrepareRinsing()
        {
            showerhead.GetComponent<Rinsing>().enabled = true;
        }
        public void PrepareSoaping()
        {
            showercap.SetActive(true);
            soap.SetActive(false);
        }

        private void LoadSoaping()
        {
            babyAnimator.SetBool("cap", true);
            soap.SetActive(true);
            soap.GetComponent<Soaping>().enabled = true;
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
            else if (!showercap.activeSelf && !soap.activeSelf)
            {
                LoadSoaping();
            }
            else if (soaping.enabled && soaping.isDone)
            {
                UnloadSoaping();
            }
            else if (rinsing.enabled && rinsing.isDone)
            {
                UnloadRinsing();
            }
            else if (puttingLotion.enabled && puttingLotion.isDone)
            {
                UnloadPuttingLotion();
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
            rinsing = showerhead.GetComponent<Rinsing>();
            soaping = soap.GetComponent<Soaping>();
            puttingLotion = lotion.GetComponent<PuttingLotion>();
            lotionImage = lotion.GetComponent<Image>();
            enabledLotionImage =
                Resources.LoadAll<Sprite>
                (
                    "Sprites/Washing/" + lotionImage.sprite.name.Remove(19)
                )[0] as Sprite;
            showerheadImage = showerhead.GetComponent<Image>();
            enabledShowerheadImage = 
                Resources.LoadAll<Sprite>
                (
                    "Sprites/Washing/" + showerheadImage.sprite.name.Remove(23)
                )[0] as Sprite;
            soapImage = soap.GetComponent<Image>();
            enabledSoapImage = 
                Resources.LoadAll<Sprite>
                (
                    "Sprites/Washing/" + soapImage.sprite.name.Remove(17)
                )[0] as Sprite;
            toothbrushImage = toothbrush.GetComponent<Image>();
        }

        private void HideBaby(bool isHide)
        {
            float transparency = (isHide ? 0f : 1f);
            var color = 
                new Color
                (
                    babyImage.color.r, 
                    babyImage.color.g, 
                    babyImage.color.b, 
                    transparency
                );

            babyImage.color = color;
        }

        private void PrepareWashing()
        {
            HideBaby(true);
            steeringWheelAnimator.SetBool("hidden", true);
            thermometer.GetComponent<SmallerAnimation>().isDisappear = false;
            thermometerAnimator.enabled = true;
            thermometer.GetComponent<RectTransform>().localScale = Vector3.one;
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

        private void UnloadPuttingLotion()
        {
            lotionImage.sprite =
                Resources.Load<Sprite>
                (
                    "Sprites/Washing/" + lotionImage.sprite.name + "_gray"
                );
            lotion.GetComponent<PuttingLotion>().enabled = false;
            toolsAnimator.SetBool("hidden", true);
            steeringWheelAnimator.SetBool("hidden", false);
            babyAnimator.SetBool("lotion", false);
        }

        private void UnloadRinsing()
        {
            showerheadImage.sprite =
                Resources.Load<Sprite>
                (
                    "Sprites/Washing/" + showerheadImage.sprite.name + "_gray"
                );
            showerhead.GetComponent<Rinsing>().enabled = false;
            lotionImage.sprite = enabledLotionImage;
            lotion.GetComponent<Button>().enabled = true;
        }

        private void UnloadSoaping()
        {
            soapImage.sprite =
                Resources.Load<Sprite>
                (
                    "Sprites/Washing/" + soapImage.sprite.name + "_gray"
                );
            soap.GetComponent<Soaping>().enabled = false;
            showerheadImage.sprite = enabledShowerheadImage;
            showerhead.GetComponent<Button>().enabled = true;
        }
    }
}