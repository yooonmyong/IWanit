using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Toast : MonoBehaviour
    {
        public void Appear()
        {
            this.gameObject.SetActive(true);
            StartCoroutine(DisappearCoroutine());
        }

        private IEnumerator DisappearCoroutine()
        {
            yield return new WaitForSeconds(1.5f);
            this.gameObject.SetActive(false);
        }
    }
}