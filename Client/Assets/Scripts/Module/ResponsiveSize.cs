using UnityEngine;
using UnityEngine.UI;

namespace Module
{
    public class ResponsiveSize : MonoBehaviour
    {
        public Canvas CanvasUI;
        public Image PopUpUI;
        public float WidthRatio, HeightRatio;

        private void Awake()
        {
            RectTransform canvasRect = CanvasUI.GetComponent<RectTransform>();
            RectTransform popupRect = PopUpUI.GetComponent<RectTransform>();

            popupRect.sizeDelta = 
                new Vector2
                ( 
                    (float)canvasRect.rect.width / WidthRatio, 
                    (float)canvasRect.rect.height / HeightRatio 
                );
        }
    }
}