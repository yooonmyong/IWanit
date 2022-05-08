using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class Spawning : MonoBehaviour
    {
        private Vector2 rangeSize;
        private Vector2 rangeCenter;
        private float zValue;

        public Vector3 GetRandomPosition(BoxCollider2D collider, float zValue)
        {
            var transform = collider.GetComponent<RectTransform>();

            rangeCenter = collider.offset;
            rangeSize.x = transform.localScale.x * collider.size.x;
            rangeSize.y = transform.localScale.y * collider.size.y;
            this.zValue = zValue;

            Vector3 randomPosition = 
                new Vector3
                (
                    Random.Range(-rangeSize.x / 2, rangeSize.x / 2), 
                    Random.Range(-rangeSize.y / 2, rangeSize.y / 2),
                    zValue
                );

            return (Vector3)rangeCenter + randomPosition;
        }
    }
}