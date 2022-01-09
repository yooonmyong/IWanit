using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller;
using UI;

namespace Parenting
{
    public class Talking : MonoBehaviour
    {
        public TalkingPopup talkingPopup;
        public InputField speechField;
        public LanguageController languageController;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Talk(speechField.text);
            }
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log("talking is clicked");
            talkingPopup.OpenPopUp();
        }

        private void Talk(string word)
        {
            languageController.CollectWord(word);
        }
    }
}