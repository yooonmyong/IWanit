using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Parenting
{
    public class Toilet : MonoBehaviour
    {
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
            Debug.Log("toilet is clicked");   
        }
    }
}