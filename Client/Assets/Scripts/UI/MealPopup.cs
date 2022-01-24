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
    public class MealPopup : MonoBehaviour, IPopUp
    {
        public Button closingButton;
        public GameObject UIelementPrefab;
        public LoadingBaby loadingBaby;
        public Meal meal;
        private uint babyMonths;
        private List<FoodInfo> listofFoodInfo;
        private Sprite foodSprite;
        private float increasingRowInterval = 0f;
        private float increasingColumnInterval = 0f;

        private void Start()
        {
            StartCoroutine(LoadFoodInfoCoroutine());
            StartCoroutine(RenderFoodCoroutine());
            closingButton.onClick.AddListener(() => ClosePopUp());
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

        private IEnumerator LoadFoodInfoCoroutine()
        {
            var URL = Config.developServer + "/Parenting/LoadFoodInfo";
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

            listofFoodInfo =
                JsonConvert.DeserializeObject<List<FoodInfo>>(response);
        }

        private IEnumerator RenderFoodCoroutine()
        {
            yield return new WaitUntil
            (
                () => listofFoodInfo != null
            );
            for (var i = 0; i < listofFoodInfo.Count; i++)
            {
                var food =
                    Instantiate
                    (
                        UIelementPrefab, 
                        new Vector3(0, 0, 0), 
                        Quaternion.identity
                    );
                var image = food.gameObject.transform.GetChild(0).gameObject;
                var name = food.gameObject.transform.GetChild(1).gameObject;
                var button = image.GetComponent<Button>();
                var rectTransform = 
                    food.gameObject.GetComponent<RectTransform>();                    

                food.AddComponent<FoodObject>().Init(listofFoodInfo[i]);
                name.GetComponent<Text>().text = listofFoodInfo[i].KoreanName;
                if 
                (
                    loadingBaby.babyObject.GetBaby().Months < 
                    listofFoodInfo[i].ProperMonths
                )
                {
                    foodSprite =
                        Resources.Load<Sprite>
                        (
                            "Sprites/Foods/" + 
                            listofFoodInfo[i].Name + 
                            "_deactivated"
                        );
                    Destroy(button);
                }
                else
                {
                    foodSprite =
                        Resources.Load<Sprite>
                        (
                            "Sprites/Foods/" + listofFoodInfo[i].Name
                        );
                    button.onClick.AddListener
                    (
                        () => 
                            meal.CheckAvailable
                            (
                                food.gameObject.GetComponent<FoodObject>()
                            )
                    );
                }
                
                image.GetComponent<Image>().sprite = foodSprite;
                food.gameObject.transform.SetParent
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