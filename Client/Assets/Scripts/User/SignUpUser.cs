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

namespace User
{
    public class SignUpUser : MonoBehaviour
    {
        public InputField UserIDField;
        public InputField UserEmailField;
        public InputField UserPWDField;
        public InputField repeatedUserPWDField;
        public GameObject ErrorPopUpUI;
        public Text ErrorMessageUI;

        public void SignUp()
        {
            StartCoroutine(SignUpCoroutine());
        }

        private IEnumerator SignUpCoroutine()
        {
            var form = new WWWForm();
            var URL = Config.developServer + "/User/SignUp";
            form.AddField("userID", UserIDField.text);
            form.AddField("userEmail", UserEmailField.text);
            form.AddField("userPWD", UserPWDField.text);
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

                ErrorMessageUI.text = errorcase.GetErrorcase(errorMessage);
                ErrorPopUpUI.SetActive(true);                
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