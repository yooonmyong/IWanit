using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
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

        private void OnEnable()
        {
            var config = new RealmConfiguration(Config.dbPath)
            {
                SchemaVersion = 1
            };
            realm = Realm.GetInstance(config);
        }

        private void Start()
        {
            StartCoroutine(SetControllerCoroutine());
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
            }
        }

        private void OnDisable()
        {
            realm.Dispose();
        }
    }
}