using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Module
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController instance;

        public static SceneController GetInstance()
        {
            if (!instance)
            {
                instance = 
                    (SceneController)GameObject
                    .FindObjectOfType(typeof(SceneController));
                if (!instance)
                {
                    Debug.LogError(
                        "There needs to be one active SceneController script" +
                        " on a GameObject in your scene."
                    );
                }
            }

            return instance;
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            AsyncOperation asyncOperation = 
                SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                Debug.Log(asyncOperation.progress * 100 + "% done");
                yield return null;
            }
        }
    }
}