using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using Module;

namespace Baby
{
    public class SettingBaby : MonoBehaviour
    {
        public InputField BabyNameField;
        public GameObject PuttingNamePanel;
        public GameObject ErrorPopUpUI;

        public void PutName()
        {
            StartCoroutine(PutNameCoroutine());
        }

        private IEnumerator PutNameCoroutine()
        {
            if (BabyNameField.text.Length < 1)
            {
                Debug.Log("Put valid baby name");
                ErrorPopUpUI.SetActive(true);
                yield return null;
            }
            else 
            {
                var form = new WWWForm();
                var URL = 
                    "http://" + Config.developServer + "/Baby/SetInitialBaby";
                form.AddField("babyName", BabyNameField.text);
                UnityWebRequest www = UnityWebRequest.Post(URL, form); 
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log($"{www.error}");
                }
                else 
                {
                    Debug.Log("Success to send name data to server!");
                }

                PuttingNamePanel.SetActive(false);
            }
        }
    }
}
