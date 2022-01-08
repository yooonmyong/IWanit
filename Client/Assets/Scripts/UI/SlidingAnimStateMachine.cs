using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SlidingAnimStateMachine : StateMachineBehaviour
    {
        override public void OnStateExit
        (
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (stateInfo.IsName("SlideDownUI"))
            {
                var talkingPopup = animator.GetComponent<TalkingPopup>();

                talkingPopup.ClosePopUp();
            }
        }
    }
}