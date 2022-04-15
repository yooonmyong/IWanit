using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parenting
{
    public class FoodObject : MonoBehaviour
    {
        private FoodInfo foodInfo;

        public void Init(FoodInfo foodInfo)
        {
            this.foodInfo = foodInfo;
        }

        public FoodInfo GetFood()
        {
            return foodInfo;
        }
    }
}