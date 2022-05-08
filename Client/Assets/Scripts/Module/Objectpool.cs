using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class Objectpool : MonoBehaviour
    {
        public GameObject gameObject;
        public int count;
        private Queue<GameObject> pool;

        private void Start()
        {
            pool = new Queue<GameObject>();
            CreatePoolObjects();
        }

        public GameObject DequeueObject()
        {
            if (GetPoolSize() == 0)
            {
                return null;
            }

            var poolObject = pool.Dequeue();
            poolObject.SetActive(true);

            return poolObject;
        }

        public void EnqueueObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
            pool.Enqueue(gameObject);
        }

        public int GetPoolSize()
        {
            return pool.Count;
        }

        private void CreatePoolObjects()
        {
            for (var i = 0; i < count; i++)
            {
                var poolObject = Instantiate(gameObject);

                poolObject.SetActive(false);
                pool.Enqueue(poolObject);
            }
        }
    }
}