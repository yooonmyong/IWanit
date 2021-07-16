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
            var URL = "http://127.0.0.1:/User/sign-up";
            form.AddField("userID", UserIDField.text);
            form.AddField("userEmail", UserEmailField.text);
            form.AddField("userPWD", UserPWDField.text);
            form.AddField("repeatedUserPWD", repeatedUserPWDField.text);
            UnityWebRequest www = UnityWebRequest.Post(URL, form);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log($"Network Error occured: {www.error}");
                if (www.responseCode > 0)
                {
                    var response = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    var error = JsonConvert.DeserializeObject<JObject>(response);
                    var errorMessage = (string)error["message"];

                    if (String.Compare(errorMessage, EnumToStringConverter.GetStringValue(Errorcase.Error1)) == 0)
                    {
                        ErrorMessageUI.text = "아이디 조건을 만족하지 않습니다.";
                    }
                    else if (String.Compare(errorMessage, EnumToStringConverter.GetStringValue(Errorcase.Error2)) == 0)
                    {
                        ErrorMessageUI.text = "비밀번호 조건을 만족하지 않습니다.";
                    }
                    else if (String.Compare(errorMessage, EnumToStringConverter.GetStringValue(Errorcase.Error3)) == 0)
                    {
                        ErrorMessageUI.text = "비밀번호 간에 일치하지 않습니다.";
                    }
                    else if (String.Compare(errorMessage, EnumToStringConverter.GetStringValue(Errorcase.Error4)) == 0)
                    {
                        ErrorMessageUI.text = "이미 사용 중인 아이디 혹은 이메일 입니다.";
                    }

                    ErrorPopUpUI.SetActive(true);
                }
            }
            else
            {
                Debug.Log("Success to send user data to server!");
            }
        }
    }
}