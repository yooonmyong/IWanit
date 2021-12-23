using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using Module;

namespace User
{
    public class SignInUser : MonoBehaviour
    {
        public GameObject ErrorPopUpUI;
        public InputField userIDField;
        public InputField userPWDField;

        public void SignIn()
        {
            StartCoroutine(SignInCoroutine());
        }

        private IEnumerator SignInCoroutine()
        {
            var form = new WWWForm();
            var URL = Config.developServer + "/User/SignIn";
            form.AddField("userID", userIDField.text);
            form.AddField("userPWD", userPWDField.text);
            UnityWebRequest www = UnityWebRequest.Post(URL, form);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log($"{www.error}");
            }
            else
            {
                Debug.Log("Success to send sign-in data to server!");
            }

            switch(www.responseCode)
            {
                case 422:
                    Debug.Log("Trying to access using invalid userID");
                    ErrorPopUpUI.SetActive(true);
                    break;
                case 500:
                    Debug.Log("Server or database error occured");
                    break;
                default:
                    Debug.Log("Success to sign in");
                    var sceneController = SceneController.GetInstance();
                    sceneController.LoadScene("GameScene");
                    break;
            }
        }
    }
}