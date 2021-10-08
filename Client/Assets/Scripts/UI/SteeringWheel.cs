using System;
using System.Collections;
using UnityEngine;
using Parenting;

namespace UI
{
    public class SteeringWheel : MonoBehaviour
    {
        public GameObject SubMenu;        
        private Vector3 previousPosition = Vector3.zero;
        private float rotatingSpeed = 1.0f;

        private void Update()
        {
            if (SubMenu.activeSelf)
            {
                this.gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<Collider2D>().enabled = true;
            }
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