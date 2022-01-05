using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Baby;
using Module;
using Parenting;

namespace UI
{
    public class PlayingPopup : MonoBehaviour, IPopUp
    {
        public Button closingButton;
        public GameObject UIelementPrefab;
        public Playing playingGameObject;
        private uint babyMonths;
        private List<PlayingInfo> listofPlayingInfo;
        private Sprite playingSprite;
        private float increasingRowInterval = 0f;
        private float increasingColumnInterval = 0f;

        private void Start()
        {
            closingButton.onClick.AddListener(() => ClosePopUp());
            StartCoroutine(LoadPlayingInfoCoroutine());
            StartCoroutine(SetMonthsCoroutine());
            StartCoroutine(RenderPlayingCoroutine());
        }

        public void OpenPopUp()
        {
            this.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        public void ClosePopUp()
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f;            
        }

        private IEnumerator LoadPlayingInfoCoroutine()
        {
            var URL = Config.developServer + "/Parenting/LoadPlayingInfo";
            UnityWebRequest www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log($"{www.error}");
            }

            var response =
                System.Text.Encoding.UTF8.GetString
                (
                    www.downloadHandler.data
                );

            listofPlayingInfo =
                JsonConvert.DeserializeObject<List<PlayingInfo>>(response);
        }

        private IEnumerator SetMonthsCoroutine()
        {
            yield return new WaitUntil
            (
                () => GameObject.Find("Baby(Clone)") != null
            );
            var baby =
                GameObject.Find("Baby(Clone)").GetComponent<BabyObject>();
            babyMonths = baby.GetBaby().Months;
        }

        private IEnumerator RenderPlayingCoroutine()
        {
            yield return new WaitUntil
            (
                () => listofPlayingInfo != null
            );
            for (var i = 0; i < listofPlayingInfo.Count; i++)
            {
                var playing =
                    Instantiate
                    (
                        UIelementPrefab, 
                        new Vector3(0, 0, 0), 
                        Quaternion.identity
                    );
                var image = playing.gameObject.transform.GetChild(0).gameObject;
                var name = playing.gameObject.transform.GetChild(1).gameObject;
                var button = image.GetComponent<Button>();
                var rectTransform = 
                    playing.gameObject.GetComponent<RectTransform>();                    

                playing.AddComponent<PlayingObject>().Init(listofPlayingInfo[i]);
                name.GetComponent<Text>().text = listofPlayingInfo[i].KoreanName;
                if (babyMonths < listofPlayingInfo[i].ProperMonths)
                {
                    playingSprite =
                        Resources.Load<Sprite>
                        (
                            "Sprites/playings/" + 
                            listofPlayingInfo[i].Name + 
                            "_deactivated"
                        );
                    Destroy(button);
                }
                else
                {
                    playingSprite =
                        Resources.Load<Sprite>
                        (
                            "Sprites/playings/" + listofPlayingInfo[i].Name
                        );
                    button.onClick.AddListener
                    (
                        () => 
                            playingGameObject.CheckAvailable
                            (
                                playing.gameObject.GetComponent<PlayingObject>()
                            )
                    );
                }
                
                image.GetComponent<Image>().sprite = playingSprite;
                playing.gameObject.transform.SetParent
                (
                    this.gameObject.transform.GetChild(1).transform
                );
                rectTransform.anchorMin = 
                    new Vector2
                    (
                        Constants.StartingValueinRow + increasingRowInterval,
                        Constants.StartingValueinColumn + increasingColumnInterval
                    );
                rectTransform.anchorMax = 
                    new Vector2
                    (
                        Constants.StartingValueinRow + increasingRowInterval,
                        Constants.StartingValueinColumn + increasingColumnInterval
                    );
                rectTransform.localPosition = Vector3.zero;
                rectTransform.anchoredPosition = Vector3.zero;
                rectTransform.localScale = new Vector3(1, 1, 1);
                if ((i + 1) % Constants.MaxElementsinaRow == 0)
                {
                    increasingRowInterval = 0;
                    increasingColumnInterval -= Constants.ColumnInterval;
                }
                else
                {
                    increasingRowInterval += Constants.RowInterval;
                }
            }
        }        
    }
}