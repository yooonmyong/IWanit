using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Module;

namespace Parenting
{
    public class Rinsing : MonoBehaviour, IDragHandler
    {
        public bool isDone;
        public Soaping soaping;
        private Animator showerheadAnimator;
        private Vector2 originalTransform;
        private RectTransform rectTransform;

        private void Start()
        {
            this.GetComponent<Button>().enabled = false;
            isDone = false;
            rectTransform = this.GetComponent<RectTransform>();
            originalTransform = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = Constants.CenterToolTransform;
            showerheadAnimator = this.GetComponent<Animator>();
            showerheadAnimator.enabled = true;
        }

        private void Update()
        {
            if (soaping.bubbles.Count <= 0)
            {
                rectTransform.anchoredPosition = originalTransform;
                showerheadAnimator.enabled = false;
                isDone = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name.Equals("Bubble(Clone)"))
            {
                soaping.bubbles.Remove(other.gameObject);
                soaping.bubblePool.EnqueueObject(other.gameObject);
            }
        }
    }
}