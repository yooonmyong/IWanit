using System;
using System.Collections;
using System.Collections.Generic;

namespace Module
{
    public sealed class WriteOnce
    {
        private Dictionary<string, bool> checkSet;
        
        public WriteOnce()
        {
            checkSet = new Dictionary<string, bool>();
        }
        
        public void AlreadyWrite(string feature)
        {
            checkSet.Add(feature, true);
        }

        public bool isAlreadyWrite(string feature)
        {
            return checkSet.ContainsKey(feature);
        }
    }
}