using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Module;

namespace Parenting
{
    public class PlayingInfo
    {
        private string name;
        private double calorie;
        private string koreanName;
        private int properMonths;
        private WriteOnce writeOnce = new WriteOnce();

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (writeOnce.isAlreadyWrite("Name"))
                {
                    throw new InvalidOperationException("Value already set");
                }

                name = value;
                writeOnce.AlreadyWrite("Name");
            }            
        }

        public double Calorie
        {
            get
            {
                return calorie;
            }

            set
            {
                if (writeOnce.isAlreadyWrite("Calorie"))
                {
                    throw new InvalidOperationException("Value already set");
                }

                calorie = value;
                writeOnce.AlreadyWrite("Calorie");
            }            
        }

        public string KoreanName
        {
            get
            {
                return koreanName;
            }

            set
            {
                if (writeOnce.isAlreadyWrite("KoreanName"))
                {
                    throw new InvalidOperationException("Value already set");
                }

                koreanName = value;
                writeOnce.AlreadyWrite("KoreanName");
            }            
        }

        public int ProperMonths
        {
            get
            {
                return properMonths;
            }

            set
            {
                if (writeOnce.isAlreadyWrite("ProperMonths"))
                {
                    throw new InvalidOperationException("Value already set");
                }

                properMonths = value;
                writeOnce.AlreadyWrite("ProperMonths");
            }
        }
    }
}