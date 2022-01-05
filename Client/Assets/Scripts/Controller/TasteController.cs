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
        private Realm realm;
        private Taste taste;
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

        private void OnDisable()
        {
            realm.Dispose();
        }
    }
}