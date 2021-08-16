using System;
using System.Collections;
using System.Collections.Generic;

namespace Baby {
    public class BabyObject {
        public String Name 
        {
            get;
        }

        public int Months
        {
            get; set;
        }

        public Dictionary<String, Skills> Level
        {
            get;
        }

        public Decimal Weight
        {
            get; set;
        }

        public Dictionary<String, AppearanceFeatures> Appearance
        {
            get;
        }

        public Dictionary<String, TemperamentFeatures> Temperament
        {
            get;
        }        
    }

    public class Skills {
        public int toilet
        {
            get; set;
        }

        public int walking
        {
            get; set;
        }        

        public int speaking
        {
            get; set;
        }        
    }

    public class AppearanceFeatures {
        public Dictionary<String, ChangeableFeatures> changeable
        {
            get;
        }

        public Dictionary<String, UnchangeableFeatures> unchangeable
        {
            get;
        }        
    }

    public class ChangeableFeatures {
        public int hairStyle
        {
            get; set;
        }

        public int clothes
        {
            get; set;
        }

        public int body
        {
            get; set;
        }        
    }

    public class UnchangeableFeatures {
        public int hairColor
        {
            get;
        }

        public int eyebrow
        {
            get;
        }

        public int eye
        {
            get;
        }

        public int nose
        {
            get;
        }

        public int lip
        {
            get;
        }      

        public int ear
        {
            get;
        }

        public int skin
        {
            get;
        }        
    }

    public class TemperamentFeatures {
        public Decimal activity
        {
            get;
        }

        public Decimal intensity
        {
            get;
        }

        public Decimal regularity
        {
            get;
        }

        public Decimal adaptability
        {
            get;
        }

        public Decimal attentionPersistence
        {
            get;
        }        
    }
}