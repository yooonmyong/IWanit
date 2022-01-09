using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Baby;
using Model;
using Module;

namespace Controller
{
    public class LanguageController : MonoBehaviour
    {
        private Realm realm;
        private Language language;
        private GameObject baby;
        private Dictionary<string, int> notyetRememberedWords;
        private Dictionary<string, int> rememberedWords;
        private List<string> alreadyRememberedWords;
        private System.Random random;

        private void OnEnable()
        {
            var config = new RealmConfiguration(Config.dbPath)
            {
                SchemaVersion = 1
            };

            realm = Realm.GetInstance(config);
            random = new System.Random();
            alreadyRememberedWords = new List<string>();
        }

        private void Start()
        {
            StartCoroutine(SetControllerCoroutine());
        }

        private void Update()
        {
            StartCoroutine(RankWordCoroutine());
        }

        private void OnApplicationQuit()
        {
            realm.Write(() =>
            {
                language.NotyetRememberedWords = 
                    Converter<int>.ConvertDictionaryToJson
                    (
                        notyetRememberedWords
                    );
            });
        }

        private void OnDisable()
        {
            realm.Dispose();
        }

        public void CollectWord(string word)
        {
            var point =
                random.Next(0, Constants.MinStandardtoRememberWord + 1);

            if (notyetRememberedWords.ContainsKey(word))
            {
                notyetRememberedWords[word] += point;
            }
            else
            {
                notyetRememberedWords.Add(word, point);
            }
        }

        private IEnumerator RankWordCoroutine()
        {
            yield return new WaitUntil
            (
                () => notyetRememberedWords != null && rememberedWords != null
            );

            foreach (var word in notyetRememberedWords)
            {
                if (word.Value >= language.StandardtoRememberWord)
                {
                    if (rememberedWords.ContainsKey(word.Key))
                    {
                        rememberedWords[word.Key]++;
                    }
                    else
                    {
                        rememberedWords.Add(word.Key, 0);
                    }

                    alreadyRememberedWords.Add(word.Key);
                }
            }
            
            foreach (var alreadyRememberedWord in alreadyRememberedWords)
            {
                notyetRememberedWords.Remove(alreadyRememberedWord);
            }

            realm.Write(() =>
            {
                language.RememberedWords =
                    Converter<int>.ConvertDictionaryToJson(rememberedWords);
            });
            alreadyRememberedWords.Clear();
        }

        private IEnumerator SetControllerCoroutine()
        {
            yield return new WaitUntil
            (
                () => GameObject.Find("Baby(Clone)") != null
            );
            baby = GameObject.Find("Baby(Clone)");

            var id = Guid.Parse(baby.GetComponent<BabyObject>().GetBaby().UUID);

            language = realm.Find<Language>(id);
            if (language == null)
            {
                Debug.Log("Create new language local database");
                realm.Write(() =>
                {
                    language = realm.Add(new Language(id));
                });
                notyetRememberedWords = new Dictionary<string, int>();
                rememberedWords = new Dictionary<string, int>();
            }
            else
            {
                notyetRememberedWords = 
                    JsonConvert.DeserializeObject<Dictionary<string, int>>
                    (
                        language.NotyetRememberedWords
                    );
                rememberedWords =
                    JsonConvert.DeserializeObject<Dictionary<string, int>>
                    (
                        language.RememberedWords
                    );
            }
        }
    }
}