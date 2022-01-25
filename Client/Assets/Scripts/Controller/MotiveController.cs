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
        public LoadingBaby loadingBaby;
        private Realm realm;
        private MotiveValue motiveValue;
        private int standardofAutomaticalUpdate = 0;
        private int termOfUpdateMotive = 0;

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

        private void Update()
        {
            StartCoroutine(UpdateMotiveCoroutine());
        }

        private void OnApplicationQuit()
        {
            realm.Write(() =>
            {
                motiveValue.motive.Fun = motiveValue.Fun;
                motiveValue.motive.Energy = motiveValue.Energy;
                motiveValue.motive.Hunger = motiveValue.Hunger;
                motiveValue.motive.Social = motiveValue.Social;
                motiveValue.motive.Stress = motiveValue.Stress;
                motiveValue.motive.Hygiene = motiveValue.Hygiene;
                motiveValue.motive.Urine = motiveValue.Urine;
            });
        }

        private void OnDisable()
        {
            realm.Dispose();
        }

        public bool IsEnergyLack()
        {
            if (motiveValue.Energy <= motiveValue.motive.LackMotive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsFunLack()
        {
            return motiveValue.Fun <= motiveValue.motive.LackMotive;
        }

        public bool IsHungerLack()
        {
            return motiveValue.Hunger <= motiveValue.motive.LackMotive;
        }

        public bool IsHygieneLack()
        {
            return motiveValue.Hygiene <= motiveValue.motive.LackMotive;
        }        

        public bool IsSocialLack()
        {
            return motiveValue.Social <= motiveValue.motive.LackMotive;
        }

        public bool IsStressFull()
        {
            return motiveValue.Stress >= Constants.FullMotive;
        }

        public bool IsUrineLack()
        {
            return motiveValue.Urine <= motiveValue.motive.LackMotive;
        }

        public void UpdateEnergy(double value)
        {
            motiveValue.Energy += value;
        }

        public void UpdateFun(double value)
        {
            motiveValue.Fun += value;
        }

        public void UpdateHunger(double value)
        {
            motiveValue.Hunger += value;
        }

        public void UpdateHygiene(double value)
        {
            motiveValue.Hygiene += value;
        }

        public void UpdateSocial(double value)
        {
            motiveValue.Social += value;
        }

        public void UpdateStress(double value)
        {
            motiveValue.Stress += value;
        }

        public void UpdateUrine(double value)
        {
            motiveValue.Urine += value;
        }

        private IEnumerator InitCoroutine()
        {
            yield return new WaitUntil
            (
                () => loadingBaby.babyObject != null
            );
            
            var id = Guid.Parse(loadingBaby.babyObject.GetBaby().UUID);
            Motive motive = realm.Find<Motive>(id);

            if (motive == null)
            {
                Debug.Log("Create new motive local database");
                double intensity =
                    Decimal.ToDouble
                    (
                        loadingBaby.babyObject
                        .GetBaby()
                        .Temperament["intensity"]
                    );
                double motiveLackPoint = 
                    (intensity * Constants.FullMotive) % 
                    (Constants.FullMotive / 2);
                realm.Write(() =>
                {
                    motive = realm.Add(new Motive(id, ++motiveLackPoint));
                });
            }

            motiveValue = new MotiveValue(motive);
        }

        private IEnumerator UpdateMotiveCoroutine()
        {
            yield return new WaitUntil
            (
                () => motiveValue != null
            );
            if (IsStressFull())
            {
            }
            {
            }

            if (termOfUpdateMotive == standardofAutomaticalUpdate)
            {
                if (motiveValue.IsMotiveLack())
                {
                    Debug.Log("Some motive lacks!");
                    motiveValue.Stress +=
                        motiveValue.motive.random.NextDouble()
                        * Constants.HandlingDigit;
                }

                motiveValue.UpdateMotiveRandomly();                
                termOfUpdateMotive = 0;
                standardofAutomaticalUpdate = 
                    motiveValue.motive.random.Next
                    (
                        Constants.MinimumofAutomaticalUpdate, 
                        Constants.MaximumofAutomaticalUpdate
                    );
            }
            else
            {
                termOfUpdateMotive++;                
            }
        }
    }
}