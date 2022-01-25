using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Module;
using UI;

namespace UI
{
    public class Bar : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = this.GetComponent<Slider>();
            slider.value = 0;
        }

        public int GetValue()
        {
            return (int)slider.value;
        }

        public void SetMaxValue(int value)
        {
            slider.maxValue = value;
        }

        public void SetValue(int value)
        {
            slider.value = value;
        }
    }
}