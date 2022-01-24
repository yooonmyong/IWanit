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
    public class TasteController : MonoBehaviour
    {
        public LoadingBaby loadingBaby;
        private Realm realm;
        private Taste taste;

        private void Awake()
        {
            var config = new RealmConfiguration(Config.dbPath)
            {
                SchemaVersion = 1
            };
            
            realm = Realm.GetInstance(config);
        }

        private void Start()
        {
            StartCoroutine(InitCoroutine());
        }

        private void OnDisable()
        {
            realm.Dispose();
        }

        private IEnumerator InitCoroutine()
        {
            yield return new WaitUntil
            (
                () => loadingBaby.babyObject != null
            );
            
            var id = Guid.Parse(loadingBaby.babyObject.GetBaby().UUID);

            taste = realm.Find<Taste>(id);
            if (taste == null)
            {
                Debug.Log("Create new taste local database");
                realm.Write(() =>
                {
                    taste = realm.Add(new Taste(id));
                });
            }
        }
    }
}