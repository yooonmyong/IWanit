using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TalkingPopup : MonoBehaviour, IPopUp
    {
        public Collider2D collider2D;
        public SlidingAnimation slidingAnimation;
        private bool doesSwipeOccur = false;
        private Vector2 firstTapPosition;
        private Vector2 secondTapPosition;
        private Vector2 swipePosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstTapPosition =
                    new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                if (collider2D.OverlapPoint(firstTapPosition))
                {
                    doesSwipeOccur = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                secondTapPosition =
                    new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                swipePosition =
                    new Vector2
                    (
                        secondTapPosition.x - firstTapPosition.x,
                        secondTapPosition.y - firstTapPosition.y
                    );
                swipePosition.Normalize();
                if 
                (
                    swipePosition.y < 0 && 
                    swipePosition.x > -0.5f && swipePosition.x < 0.5f
                )
                {
                    slidingAnimation.ShoworHideUI(true);
                }
            }
        }

        public void OpenPopUp()
        {
            this.gameObject.SetActive(true);            
            slidingAnimation.ShoworHideUI(false);
        }

        public void ClosePopUp()
        {
            this.gameObject.SetActive(false);
        }
    }
}