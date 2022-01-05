using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parenting
{
    public class PlayingObject : MonoBehaviour
    {
        private PlayingInfo playingInfo;

        public void Init(PlayingInfo playingInfo)
        {
            this.playingInfo = playingInfo;
        }

        public PlayingInfo GetPlaying()
        {
            return playingInfo;
        }
    }
}