using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Module;
using UI;

namespace Parenting
{
    public class Brushing : MonoBehaviour, IDragHandler
    {
        public bool isDone;
        public GameObject bubblePrefab;
        public Bar bar;
        private Vector2 originalTransform;
        private RectTransform rectTransform;
        private List<GameObject> generatedBubbles;

        private void Start()
        {
            bar.gameObject.SetActive(true);
            isDone = false;
            generatedBubbles = new List<GameObject>();
            rectTransform = this.GetComponent<RectTransform>();
            originalTransform = rectTransform.anchoredPosition;
            if (this.name.Equals("Gauze"))
            {
                rectTransform.anchoredPosition = Constants.LeftToolTransform;
            }
            else if (this.name.Equals("Toothbrush"))
            {
                rectTransform.anchoredPosition = Constants.RightToolTransform;
                this.GetComponent<Button>().enabled = false;
            }

            bar.SetMaxValue(Constants.BrushingEnough);
        }

        private void Update()
        {
            if (bar.GetValue() >= Constants.BrushingEnough)
            {
                foreach (var bubble in generatedBubbles)
                {
                    Destroy(bubble);
                }

                bar.gameObject.SetActive(false);
                rectTransform.anchoredPosition = originalTransform;
                if (this.name.Equals("Toothbrush"))
                {
                    this.GetComponent<Button>().enabled = true;
                }

                isDone = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if 
            (
                this.name.Equals("Toothbrush") && 
                collision.gameObject.name.Equals("Teeth") 
                ||
                this.name.Equals("Gauze") && 
                collision.gameObject.name.Equals("Others")
            )
            {
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    Vector2 hitPoint = contact.point;
                    var bubble = 
                        Instantiate
                        (
                            bubblePrefab, 
                            new Vector3(hitPoint.x, hitPoint.y, 1), 
                            Quaternion.identity
                        );

                    generatedBubbles.Add(bubble);
                }

                bar.SetValue(bar.GetValue() + 1);
            }
        }
    }
}