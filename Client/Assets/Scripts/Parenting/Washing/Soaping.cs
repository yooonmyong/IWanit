using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Module;

namespace Parenting
{
    public class Soaping : MonoBehaviour, IDragHandler
    {
        public bool isDone;
        public Objectpool bubblePool;
        public List<GameObject> bubbles;
        public Spawning spawning;
        private Animator soapAnimator;
        private int bubblesOnHead;
        private int bubblesOnBody;
        private RectTransform rectTransform;
        private Vector2 originalTransform;

        private void Start()
        {
            isDone = false;
            rectTransform = this.GetComponent<RectTransform>();
            originalTransform = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = Constants.CenterToolTransform;
            if (this.name.Equals("Soap"))
            {
                this.GetComponent<Button>().enabled = false;
                soapAnimator = this.GetComponent<Animator>();
                bubblesOnHead = 0;
                bubblesOnBody = 0;
                bubbles = new List<GameObject>();
            }
        }

        private void Update()
        {
            if 
            (
                this.name.Equals("Soap") && 
                bubblesOnHead + bubblesOnBody >= 
                Constants.SoapingHeadEnough + Constants.SoapingBodyEnough
            )
            {
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
            if (this.name.Equals("Showercap") && other.name.Equals("Head"))
            {
                this.gameObject.SetActive(false);
            }
            else if (this.name.Equals("Soap"))
            {
                if (other.name.Equals("Head") || other.name.Equals("Body"))
                {
                    soapAnimator.SetTrigger("pump");
                    MakeBubble(other);
                    if
                    (
                        bubblesOnHead < Constants.SoapingHeadEnough &&
                        other.name.Equals("Head")
                    )
                    {
                        bubblesOnHead++;
                    }
                    else if
                    (
                        bubblesOnBody < Constants.SoapingBodyEnough &&
                        other.name.Equals("Body")
                    )
                    {
                        bubblesOnBody++;
                    }
                }
            }
        }

        private void MakeBubble(Collider2D collider)
        {
            var amountofBubbles = Random.Range(1, 3);
            BoxCollider2D boxCollider = collider as BoxCollider2D;

            for (var i = 0; i < amountofBubbles; i++)
            {
                var bubble = bubblePool.DequeueObject();

                bubble.transform.position = 
                    spawning.GetRandomPosition(boxCollider, 1.0f);
                bubble.transform.rotation = Quaternion.identity;
                bubble.transform.localScale = Constants.BubbleScale;
                bubbles.Add(bubble);
            }
        }
    }
}