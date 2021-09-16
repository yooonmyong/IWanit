using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Realms;
using Model;
using Module;
using Baby;

namespace Controller
{
    public class TimeController : MonoBehaviour
    {
        private Realm realm;
        public Model.Time time;
        private GameObject baby;
        private const int oneDay = 86400;
        private const int oneYear = 365;
        private const int speedOfElapsedTime = 5000;
        private int elapsedDays;
        private float elapsedTime;
        private bool isPaused;

        private void OnEnable()
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
            StartCoroutine(SetController());
        }

        private IEnumerator SetController()
        {
            yield return new WaitUntil
            (
                () => GameObject.Find("Baby(Clone)") != null
            );
            baby = GameObject.Find("Baby(Clone)");
            var id = Guid.Parse(baby.GetComponent<BabyObject>().GetBaby().UUID);
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
            elapsedDays = 364;
        }

        private void Update()
        {
            if (!isPaused)
            {
                elapsedTime += UnityEngine.Time.deltaTime * speedOfElapsedTime;
                if (Math.Round((Decimal)elapsedTime) >= oneDay)
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
                    if (elapsedDays == oneYear)
                    {
                        baby.GetComponent<BabyObject>().GetBaby().Age++;
                        Debug.Log(baby.GetComponent<BabyObject>().GetBaby().Age + "살");
                        elapsedDays = 0;
                        // 생일 이벤트 발생
                    }
                }
                catch (NullReferenceException exception)
                {
                }
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            // 홈 버튼을 눌러서 게임을 잠깐 비활성화 했을 때 실행되는 함수
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
    }
}