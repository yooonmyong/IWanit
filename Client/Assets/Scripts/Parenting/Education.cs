using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UI;

namespace Parenting
{
    public class Education : MonoBehaviour
    {
        public GameObject SubMenu;

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

        private void OnMouseUpAsButton()
        {
            Debug.Log("education is clicked");
            SubMenu.GetComponent<PopUp>().OpenPopUp();
        }
    }
}