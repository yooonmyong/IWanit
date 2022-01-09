using System;
using UnityEngine;
using UnityEngine.UI;

namespace Module
{
    public static class UIExtension
    {
        public static void Clear(this InputField inputField)
        {
            inputField.Select();
            inputField.text = "";
        }
    }
}