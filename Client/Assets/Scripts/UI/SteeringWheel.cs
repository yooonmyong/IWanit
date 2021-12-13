using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Parenting;

namespace UI
{
    public class SteeringWheel : MonoBehaviour
    {
        private Vector3 previousPosition = Vector3.zero;
        private float rotatingSpeed = 1.0f;

        private void Update()
        {
            var popupList = new List<GameObject>();
            
            popupList = 
                new List<GameObject>
                (
                    GameObject.FindGameObjectsWithTag("Popup")
                );
            this.gameObject.GetComponent<Collider2D>().enabled = 
                (popupList.Any()) ? false : true;
        }

        private void OnMouseDrag()
        {
            Vector3 positionDelta = Input.mousePosition - previousPosition;

            transform.Rotate
            (
                transform.forward,
                -Vector3.Dot(positionDelta, Camera.main.transform.right)
                    * Time.deltaTime * rotatingSpeed,
                Space.World
            );
        }
    }
}