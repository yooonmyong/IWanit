using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Module;

namespace Account
{
    public class SignInUser : MonoBehaviour
    {
        public InputField UserIDField;
        public InputField UserPWDField;
        public GameObject ErrorPopUpUI;

        public void SignIn()
        {
            StartCoroutine(SignInCoroutine());
        }

        private IEnumerator SignInCoroutine()
        {
            var form = new WWWForm();
            var URL = "http://127.0.0.1:/User/sign-in";
            form.AddField("userID", UserIDField.text);
            form.AddField("userPWD", UserPWDField.text);
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

            if (www.responseCode == 403)
            {
                ErrorPopUpUI.SetActive(true);
            }
            else
            {
                var response = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                var result = JsonConvert.DeserializeObject<JObject>(response);
                Debug.Log("Success to sign in");
            }
        }
    }
}