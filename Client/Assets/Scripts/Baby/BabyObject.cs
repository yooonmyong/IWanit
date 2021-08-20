using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baby
{
    public class BabyObject : MonoBehaviour
    {
        private BabyInfo babyInfo;

        public void Init(BabyInfo babyInfo, Transform parentTransform)
        {
            this.babyInfo = babyInfo;
            this.transform.SetParent(parentTransform);
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.transform.localScale += new Vector3(50, 50, 50);
        }

        public BabyInfo GetBaby()
        {
            return babyInfo;
        }
    }
}