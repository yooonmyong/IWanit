using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Module;

namespace Baby
{
    public class BabyObject : MonoBehaviour
    {
        private Animator animator;
        private BabyInfo babyInfo;

        private void OnMouseEnter()
        {
            animator.SetBool("smile", true);
        }

        private void OnMouseExit()
        {
            animator.SetBool("smile", false);
        }

        private void OnApplicationQuit()
        {
            StartCoroutine(SaveBabyInfoCoroutine());
        }

        public void Init(BabyInfo babyInfo)
        {
            this.babyInfo = babyInfo;
            this.transform.localPosition = Constants.InitializedTransform;
            animator = this.gameObject.GetComponent<Animator>();
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        public BabyInfo GetBaby()
        {
            return babyInfo;
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