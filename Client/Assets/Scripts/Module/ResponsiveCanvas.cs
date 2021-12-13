using UnityEngine;
using UnityEngine.UI;

namespace Module
{
    public class ResponsiveCanvas : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.GetComponent<CanvasScaler>().referenceResolution = 
                new Vector2(Screen.width, Screen.height);
        }
    }
}