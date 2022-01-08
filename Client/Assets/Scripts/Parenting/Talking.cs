using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;

namespace Parenting
{
    public class Talking : MonoBehaviour
    {
        public TalkingPopup talkingPopup;
        public InputField speechField;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Talk();
            }
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("talking is clicked");
            talkingPopup.OpenPopUp();
        }

        private void Talk()
        {
        }
    }
}