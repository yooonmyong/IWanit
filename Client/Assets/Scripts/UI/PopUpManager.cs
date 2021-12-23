using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{    
    public class PopUpManager : MonoBehaviour 
    {
        public Graphic steeringWheel;
        public PopUp educationPopup;
        public PopUp mealPopup;
        public PopUp playingPopup;
        public PopUp settingNamePopup;
        private Stack<PopUp> activePopupStack;
        private List<PopUp> popupList;
        private Collider2D[] colliders;

        private void Awake()
        {
            activePopupStack = new Stack<PopUp>();
            Init();
        }

        private void Update()
        {
            if (activePopupStack.Any())
            {
                foreach (var collider in colliders)
                {
                    collider.enabled = false;
                }
                steeringWheel.raycastTarget = false;
            }
            else
            {
                foreach (var collider in colliders)
                {
                    collider.enabled = true;
                }
                steeringWheel.raycastTarget = true;
            }
        }

        public void ColliderClickAction(PopUp popUp)
        {
            if (popupList.Contains(popUp))
            {
                OpenPopUp(popUp);
            }
            else
            {
                Debug.Log("Can't open invalid popup");
            }
        }

        private void OpenPopUp(PopUp popup)
        {
            activePopupStack.Push(popup);
            popup.gameObject.SetActive(true);
        }

        public void ClosePopUp(PopUp popup)
        {
            activePopupStack.Pop();
            popup.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        private void Init()
        {
            colliders = 
                steeringWheel.gameObject.GetComponentsInChildren<Collider2D>();
            popupList = new List<PopUp>()
            {
                educationPopup, mealPopup, playingPopup
            };
            foreach (var popup in popupList)
            {
                popup.closingButton.onClick.AddListener
                (
                    () => ClosePopUp(popup)
                );
            }
        }
    }
}