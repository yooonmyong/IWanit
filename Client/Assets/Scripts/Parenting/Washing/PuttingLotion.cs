using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Module;

namespace Parenting
{
    public class PuttingLotion : MonoBehaviour, IDragHandler
    {
        public bool isDone;
        public BoxCollider2D[] cleanEffectColliders;
        public GameObject cleanEffectPrefab;
        public Spawning spawning;
        public Objectpool cleanEffectPool;
        private Animator lotionAnimator;
        private int countingPuttingLotion;
        private List<GameObject> cleanEffects;
        private Vector2 originalTransform;
        private RectTransform rectTransform;

        private void Start()
        {
            this.GetComponent<Button>().enabled = false;
            isDone = false;
            rectTransform = this.GetComponent<RectTransform>();
            originalTransform = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = Constants.CenterToolTransform;
            lotionAnimator = this.GetComponent<Animator>();
            countingPuttingLotion = 0;
            cleanEffects = new List<GameObject>();
        }

        private void Update()
        {
            if (cleanEffectPool.count == 0)
            {
                foreach (var cleanEffect in cleanEffects)
                {
                    cleanEffectPool.EnqueueObject(cleanEffect);
                }

                rectTransform.anchoredPosition = originalTransform;
                isDone = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name.Equals("Body"))
            {
                lotionAnimator.SetTrigger("pump");
                countingPuttingLotion++;
                var index = 
                    countingPuttingLotion % Constants.triggerCleanEffect;

                if (countingPuttingLotion > 0 && index == 0)
                {
                    var settingLeftorRight = 
                        (countingPuttingLotion / Constants.triggerCleanEffect) 
                        % 2;

                    MakeCleanEffect(settingLeftorRight);
                }
            }
        }

        private void MakeCleanEffect(int leftOrRight)
        {
            // WIP
            var cleanEffect =
                spawning.SpawnRandomPosition
                (
                    cleanEffectPrefab, cleanEffectColliders[leftOrRight], 1.0f
                );
            
            cleanEffects.Add(cleanEffect);
        }
    }
}