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

namespace Baby
{
    public class LoadingBaby : MonoBehaviour
    {
        public GameObject PuttingNamePanel;
        public GameObject Baby;

        public void Start()
        {
            StartCoroutine(LoadBabyInfoCoroutine());
        }

        public IEnumerator LoadBabyInfoCoroutine()
        {
            var URL = Config.developServer + "/Baby/LoadBabyInfo";
            UnityWebRequest www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log($"{www.error}");
            }

            if (www.responseCode == 404)
            {
                Debug.Log("Failed to load baby info");
                PuttingNamePanel.SetActive(true);
            }
            else
            {
                var response =
                    System.Text.Encoding.UTF8.GetString
                    (
                        www.downloadHandler.data
                    );
                var babyInfo =
                    JsonConvert.DeserializeObject<BabyInfo>(response);
                Baby = 
                    Instantiate
                    (
                        Baby, new Vector3(0, 0, 0), Quaternion.identity
                    );
                Transform canvasTransform = 
                    GameObject.Find("GameCanvas").transform;
                Baby.AddComponent<BabyObject>().Init(babyInfo, canvasTransform);
            }
        }
    }
}