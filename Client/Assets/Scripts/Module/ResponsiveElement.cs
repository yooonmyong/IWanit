using UnityEngine;
using UnityEngine.UI;

namespace Module
{
    public class ResponsiveElement : MonoBehaviour
    {
        public float WidthRatio, HeightRatio;

        private void Awake()
        {
            RectTransform UIRect = 
                this.gameObject.GetComponent<RectTransform>();

            UIRect.sizeDelta = 
                new Vector2
                ( 
                    (float)Screen.width / WidthRatio, 
                    (float)Screen.height / HeightRatio 
                );
        }
    }
}