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
        private float rotatingSpeed = 1.0f;

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