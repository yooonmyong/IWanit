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
using UI;

namespace User
{
    public class SignUpUser : MonoBehaviour
    {
        public InputField userIDField;
        public InputField userEmailField;
        public InputField userPWDField;
        public InputField repeatedUserPWDField;
        public Toast toastPopup;
        public Text tostMessage;

        public void SignUp()
        {
            StartCoroutine(SignUpCoroutine());
        }

        private IEnumerator SignUpCoroutine()
        {
            var form = new WWWForm();
            var URL = Config.developServer + "/User/SignUp";
            form.AddField("userID", userIDField.text);
            form.AddField("userEmail", userEmailField.text);
            form.AddField("userPWD", userPWDField.text);
            form.AddField("repeatedUserPWD", repeatedUserPWDField.text);
            UnityWebRequest www = UnityWebRequest.Post(URL, form);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log($"Network Error occured: {www.error}");
                var response = 
                    System.Text.Encoding.UTF8.GetString(
                        www.downloadHandler.data
                    );
                Debug.Log(response);
                var error = JsonConvert.DeserializeObject<JObject>(response);
                var errorMessage = (string)error["message"];
                var errorcase = new Errorcase();

                tostMessage.text = errorcase.GetErrorcase(errorMessage);
                toastPopup.Appear();
                userPWDField.Clear();
                repeatedUserPWDField.Clear();
            }
            else
            {
                Debug.Log("Success to send user data to server!");
                var sceneController = SceneController.GetInstance();
                sceneController.LoadScene("SignInScene");
            }
        }
    }
}