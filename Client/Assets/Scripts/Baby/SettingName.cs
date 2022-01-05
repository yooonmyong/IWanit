using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using Module;
using UI;

namespace Baby
{
    public class SettingName : MonoBehaviour
    {
        public InputField babyNameField;
        public Toast toastPopup;
        public SettingNamePopup settingNamePopup;
        public LoadingBaby loadingBaby;

        public void SetName()
        {
            StartCoroutine(SetNameCoroutine());
        }

        private IEnumerator SetNameCoroutine()
        {
            if (babyNameField.text.Length < 1)
            {
                Debug.Log("Put valid baby name");
                toastPopup.Appear();
                yield return null;
            }
            else 
            {
                var form = new WWWForm();
                var URL = Config.developServer + "/Baby/SetInitialBaby";
                form.AddField("babyName", babyNameField.text);
                UnityWebRequest www = UnityWebRequest.Post(URL, form); 
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log($"{www.error}");
                }
                else 
                {
                    Debug.Log("Success to send name data to server!");
                    settingNamePopup.ClosePopUp();
                    StartCoroutine(loadingBaby.LoadBabyInfoCoroutine());
                }
            }
        }
    }
}
