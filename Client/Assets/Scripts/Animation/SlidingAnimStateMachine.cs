using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Animation
{
    public class SlidingAnimStateMachine : StateMachineBehaviour
    {
        override public void OnStateExit
        (
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (stateInfo.IsName("Slide down"))
            {
                var talkingPopup = animator.GetComponent<TalkingPopup>();

                talkingPopup.ClosePopUp();
            }
        }
    }
}