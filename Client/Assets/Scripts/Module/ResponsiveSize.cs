using UnityEngine;
using UnityEngine.UI;

namespace Module
{
    public class ResponsiveSize : MonoBehaviour
    {
        public Canvas CanvasUI;
        public Image PopUpUI;

        private void Awake()
        {
            RectTransform canvasRect = CanvasUI.GetComponent<RectTransform>();
            RectTransform popupRect = PopUpUI.GetComponent<RectTransform>();

            popupRect.sizeDelta = new Vector2( canvasRect.rect.width, canvasRect.rect.height );
        }
    }
}