using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Baby 
{
    public class BabyInfo
    {
        public string ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public int Months
        {
            get; set;
        }

        public Dictionary<string, int> Level
        {
            get; set;
        }

        public Decimal Weight
        {
            get; set;
        }

        public Dictionary<string, Dictionary<string, int>> Appearance
        {
            get; set;
        }

        public Dictionary<string, Decimal> Temperament
        {
            get; set;
        }
    }
}