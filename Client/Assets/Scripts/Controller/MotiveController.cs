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
    public class MotiveController : MonoBehaviour
    {
        private Realm realm;
        private Motive motive;
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
            StartCoroutine(SetController());
        }

        private IEnumerator SetController()
        {
            yield return new WaitUntil
            (
                () => GameObject.Find("Baby(Clone)") != null
            );
            baby = GameObject.Find("Baby(Clone)");
            var intensity =
                baby
                    .GetComponent<BabyObject>()
                    .GetBaby()
                    .Temperament["intensity"];

            var id = Guid.Parse(baby.GetComponent<BabyObject>().GetBaby().UUID);
            motive = realm.Find<Motive>(id);
            if (motive == null)
            {
                Debug.Log("Create new motive local database");
                realm.Write(() =>
                {
                    motive = realm.Add(new Motive(id, intensity));
                });
            }            
        }

        private void OnDisable()
        {
            realm.Dispose();
        }
    }
}