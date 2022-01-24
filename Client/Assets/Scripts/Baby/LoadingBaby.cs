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
using Controller;
using UI;

namespace Baby
{
    public class LoadingBaby : MonoBehaviour
    {
        public GameObject babyPrefab;
        public BabyObject babyObject;
        public Text date;
        public TimeController timeController;
        public SettingNamePopup settingNamePopup;
        private BabyInfo babyInfo;

        private void Awake()
        {
            StartCoroutine(LoadBabyInfoCoroutine());
        }

        private void Update()
        {
            try
            {
                date.text = "D+" + timeController.time.ElapsedDays;
            }
            catch (NullReferenceException exception)
            {
            }
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
                settingNamePopup.OpenPopUp();
            }
            else
            {
                var response =
                    System.Text.Encoding.UTF8.GetString
                    (
                        www.downloadHandler.data
                    );

                babyInfo =
                    JsonConvert.DeserializeObject<BabyInfo>(response);
                babyPrefab =
                    Instantiate
                    (
                        babyPrefab, new Vector3(0, 0, 0), Quaternion.identity
                    );
                RenderBaby();
                babyPrefab.AddComponent<BabyObject>().Init(babyInfo);
                babyObject = babyPrefab.GetComponent<BabyObject>();
            }
        }

        private void RenderBaby()
        {
            var hairStyle = baby.gameObject.transform.GetChild(0).gameObject;
            var eyebrow = baby.gameObject.transform.GetChild(1).gameObject;
            var eye = baby.gameObject.transform.GetChild(2).gameObject;
            var nose = baby.gameObject.transform.GetChild(3).gameObject;
            var lip = baby.gameObject.transform.GetChild(4).gameObject;
            var ear = baby.gameObject.transform.GetChild(5).gameObject;
            var clothes = baby.gameObject.transform.GetChild(6).gameObject;

            var hairStyleSprite = 
                Resources.Load<Sprite>
                (
                    "Sprites/HairStyles/" 
                    + babyInfo.Appearance["changeable"]["hairStyle"]
                );
            var eyebrowSprite = 
                Resources.Load<Sprite>
                (
                    "Sprites/Eyebrows/" 
                    + babyInfo.Appearance["unchangeable"]["eyebrow"]
                );
            var eyeSprite = 
                Resources.Load<Sprite>
                (
                    "Sprites/Eyes/" 
                    + babyInfo.Appearance["unchangeable"]["eye"]
                );
            var noseSprite = 
                Resources.Load<Sprite>
                (
                    "Sprites/Noses/" 
                    + babyInfo.Appearance["unchangeable"]["nose"]
                );
            var lipSprite = 
                Resources.Load<Sprite>
                (
                    "Sprites/Lips/" 
                    + babyInfo.Appearance["unchangeable"]["lip"]
                );
            var earSprite = 
                Resources.Load<Sprite>
                (
                    "Sprites/Ears/" 
                    + babyInfo.Appearance["unchangeable"]["ear"]
                );
            var clothesSprite = 
                Resources.Load<Sprite>
                (
                    "Sprites/Clothes/" 
                    + babyInfo.Appearance["changeable"]["clothes"]
                );

            hairStyle.GetComponent<SpriteRenderer>().sprite = hairStyleSprite;
            eyebrow.GetComponent<SpriteRenderer>().sprite = eyebrowSprite;
            eye.GetComponent<SpriteRenderer>().sprite = eyeSprite;
            nose.GetComponent<SpriteRenderer>().sprite = noseSprite;
            lip.GetComponent<SpriteRenderer>().sprite = lipSprite;
            ear.GetComponent<SpriteRenderer>().sprite = earSprite;
            clothes.GetComponent<SpriteRenderer>().sprite = clothesSprite;
        }
    }
}