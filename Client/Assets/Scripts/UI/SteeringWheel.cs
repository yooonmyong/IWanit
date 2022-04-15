using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Parenting;

namespace UI
{
    public class SteeringWheel : MonoBehaviour, IDragHandler
    {
        public GameObject Popups;
        public bool isPopupActive = false;
        private Animator animator;
        private float rotatingSpeed = 1.0f;
        private Collider2D[] colliders;

        private void Start()
        {
            colliders = this.gameObject.GetComponentsInChildren<Collider2D>();
            animator = this.gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
            foreach (Transform transform in Popups.transform)
            {
                if 
                (
                    animator.GetBool("hidden") ||
                    (
                        transform.gameObject != Popups.gameObject &&
                        transform.gameObject.activeSelf
                    )
                )
                {
                    isPopupActive = true;
                    break;                    
                }
                else
                {
                    isPopupActive = false;
                }
            }

            foreach (var collider in colliders)
            {
                collider.enabled = !isPopupActive;
            }

            this.gameObject.GetComponent<Graphic>().raycastTarget = 
                !isPopupActive;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 position = eventData.position;
            transform.Rotate
            (
                transform.forward,
                -Vector3.Dot(position, Camera.main.transform.right)
                    * Time.deltaTime * rotatingSpeed,
                Space.World
            );
        }
    }
}