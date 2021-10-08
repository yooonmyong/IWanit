using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Parenting
{
    public class Meal : MonoBehaviour
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
            Debug.Log("meal is clicked");
            SubMenu.GetComponent<PopUp>().OpenPopUp();
        }
    }
}