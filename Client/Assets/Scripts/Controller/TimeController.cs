using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Realms;
using Model;
using Module;
using Baby;
using UI;

namespace Controller
{
    public class TimeController : MonoBehaviour
    {
        public LoadingBaby loadingBaby;
        public Model.Time time;
        public SteeringWheel steeringWheel;
        private Realm realm;
        private int elapsedDays;
        private float elapsedTime;
        private bool isPaused;

        private void Awake()
        {
            var config = new RealmConfiguration(Config.dbPath)
            {
                SchemaVersion = 1
            };
            
            realm = Realm.GetInstance(config);
            isPaused = false;
        }

        private void Start()
        {
            StartCoroutine(InitCoroutine());
        }

        private void Update()
        {
            isPaused = steeringWheel.isPopupActive;
            if (!isPaused)
            {
                elapsedTime += 
                    UnityEngine.Time.deltaTime * Constants.SpeedOfElapsedTime;
                if (Math.Round((Decimal)elapsedTime) >= Constants.OneDay)
                {
                    elapsedDays++;
                    realm.Write(() =>
                    {
                        time.ElapsedDays++;
                    });
                    elapsedTime = 0.0f;
                }

                try
                {
                    if (elapsedDays == Constants.OneMonths)
                    {
                        loadingBaby.babyObject.GetBaby().Months++;
                        elapsedDays = 0;
                    }
                }
                catch (NullReferenceException exception)
                {
                }
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            isPaused = pauseStatus;
        }

        private void OnApplicationQuit()
        {
            realm.Write(() =>
            {
                time.CurrentTime = elapsedTime;
            });
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

            time = realm.Find<Model.Time>(id);
            if (time == null)
            {
                Debug.Log("Create new time local database");
                realm.Write(() =>
                {
                    time = realm.Add(new Model.Time(id));
                });
            }

            elapsedTime = time.CurrentTime;
            elapsedDays = time.ElapsedDays;
        }
    }
}