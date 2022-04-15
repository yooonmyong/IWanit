using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallerAnimation : MonoBehaviour
{
    public bool isDisappear = false;
    private float time;

    private void Update()
    {
        if (isDisappear)
        {
            transform.localScale = Vector3.one * (1 - time);
            if (time > 1f)
            {
                time = 0;
                gameObject.SetActive(false);
            }

            time += Time.deltaTime;
        }
    }
}
