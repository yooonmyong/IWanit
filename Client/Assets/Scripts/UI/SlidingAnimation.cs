using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class SlidingAnimation : MonoBehaviour
{
    public Animator animator;

    public void ShoworHideUI(bool isHidden)
    {
        animator.SetBool("hidden", isHidden);
    }
}
