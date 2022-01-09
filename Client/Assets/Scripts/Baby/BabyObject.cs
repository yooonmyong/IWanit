using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Module;

namespace Baby
{
    public class BabyObject : MonoBehaviour
    {
        private BabyInfo babyInfo;
        
        public void Init(BabyInfo babyInfo)
        {
            this.babyInfo = babyInfo;
            this.transform.localPosition = new Vector3(0, 0, 1);
        }

        public BabyInfo GetBaby()
        {
            return babyInfo;
        }

        private void OnApplicationQuit()
        {
            StartCoroutine(SaveBabyInfoCoroutine());
        }

        private IEnumerator SaveBabyInfoCoroutine()
        {
            var form = new WWWForm();
            var URL = Config.developServer + "/Baby/SaveBabyInfo";
            form.AddField("months", babyInfo.Months.ToString());
            form.AddField
            (
                "level", 
                Converter<int>.ConvertDictionaryToJson(babyInfo.Level)
            );
            form.AddField("weight", babyInfo.Weight.ToString());
            UnityWebRequest www = UnityWebRequest.Post(URL, form);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log($"{www.error}");
            }            
        }
    }
}