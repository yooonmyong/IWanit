using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class BiggerAnimation : MonoBehaviour
{
    public int currentState = Constants.Stop;
    private float time;

    private void Update()
    {
        if (currentState == Constants.ToBigger)
        {
            transform.localScale = Vector3.one * (1 + time);
            time += Time.deltaTime;
            if (time > 1f)
            {
                time = 0;
                currentState = Constants.Stop;
            }
        }
        else if (currentState == Constants.BackTotheFirsttime)
        {
            transform.localScale = Vector3.one * (1 - time);
            time += Time.deltaTime;
            if (time > 1f)
            {
                time = 0;
                currentState = Constants.Stop;
            }
        }
    }
}
