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
        private MotiveValue motiveValue;
        private GameObject baby;
        private int standardofAutomaticalUpdate = 0;
        private int termOfUpdateMotive = 0;
        private bool isTantrum = false;

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

        public bool DoesEnergyLack()
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

        public bool DoesFunLack()
        {
            if (motiveValue.Fun <= motiveValue.motive.LackMotive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesHungerLack()
        {
            if (motiveValue.Hunger <= motiveValue.motive.LackMotive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesHygieneLack()
        {
            if (motiveValue.Hygiene <= motiveValue.motive.LackMotive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }        

        public bool DoesSocialLack()
        {
            if (motiveValue.Social <= motiveValue.motive.LackMotive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesStressFull()
        {
            if (motiveValue.Stress <= Constants.FullMotive)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DoesUrineLack()
        {
            if (motiveValue.Urine <= motiveValue.motive.LackMotive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private IEnumerator SetControllerCoroutine()
        {
            yield return new WaitUntil
            (
                () => GameObject.Find("Baby(Clone)") != null
            );
            baby = GameObject.Find("Baby(Clone)");
            var id = Guid.Parse(baby.GetComponent<BabyObject>().GetBaby().UUID);
            Motive motive = realm.Find<Motive>(id);
            if (motive == null)
            {
                Debug.Log("Create new motive local database");
                double intensity =
                    Decimal.ToDouble(baby
                        .GetComponent<BabyObject>()
                        .GetBaby()
                        .Temperament["intensity"]);
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
            if (DoesStressFull())
            {
                isTantrum = true;
            }

            if (termOfUpdateMotive == standardofAutomaticalUpdate)
            {
                if (motiveValue.DoesMotiveLack())
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