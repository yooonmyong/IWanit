using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parenting
{
    public class Toilet : MonoBehaviour
    {
        public GameObject SubMenu;

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

        private void OnMouseUpAsButton()
        {
            Debug.Log("toilet is clicked");   
        }
    }
}