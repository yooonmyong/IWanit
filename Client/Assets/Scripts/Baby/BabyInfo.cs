using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Module;

namespace Baby 
{
    public class BabyInfo
    {
        private string id;
        private string uuid;
        private string name;
        private Dictionary<string, int> level;
        private Decimal weight;
        private Dictionary<string, Decimal> temperament;
        private Dictionary<string, Dictionary<string, string>> appearance;
        private WriteOnce writeOnce = new WriteOnce();

        public string ID
        {
            get
            {
                return id;
            }

            set
            {
                if (writeOnce.isAlreadyWrite("ID"))
                {
                    throw new InvalidOperationException("Value already set");
                }

                id = value;
                writeOnce.AlreadyWrite("ID");
            }
        }

        public string UUID
        {
            get
            {
                return uuid;
            }

            set
            {
                if (writeOnce.isAlreadyWrite("UUID"))
                {
                    throw new InvalidOperationException("Value already set");
                }

                uuid = value;
                writeOnce.AlreadyWrite("UUID");
            }
        }        

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

        public uint Months
        {
            get; set;
        }

        public Dictionary<string, int> Level
        {
            get
            {
                return level;
            }

            set
            {
                if (writeOnce.isAlreadyWrite("Level"))
                {
                    throw new InvalidOperationException
                    (
                        "Use `'UpdateLevel(string, int)`'"
                    );
                }

                level = value;
                writeOnce.AlreadyWrite("Level");
            }
        }

        public void UpdateLevel(string skill, int value)
        {
            string[] skills = { "toilet", "walking", "speaking" };

            if (skills.Any(skill.Contains))
            {
                level[skill] = value;
            }
            else
            {
                throw new FormatException("Invalid skill");
            }
        }

        public Decimal Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException
                    (
                        "Weight must be bigger than 0"
                    );
                }

                weight = value;
            }
        }

        public Dictionary<string, Dictionary<string, string>> Appearance
        {
            get
            {
                return appearance;
            }

            set
            {
                if (writeOnce.isAlreadyWrite("Appearance"))
                {
                    throw new InvalidOperationException
                    (
                        "Use `'UpdateAppearance(string, string, string)`'"
                    );
                }

                appearance = value;
                writeOnce.AlreadyWrite("Appearance");
            }
        }

        public void UpdateAppearance(string kind, string feature, string value)
        {
            if 
            (
                kind.Equals("unchangeable") 
                && writeOnce.isAlreadyWrite("Appearance")
            )
            {
                throw new InvalidOperationException("Value already set");
            }
            else
            {
                if 
                (
                    appearance.ContainsKey(kind) 
                    && appearance[kind].ContainsKey(feature)
                )
                {
                    appearance[kind][feature] = value;
                }
                else
                {
                    throw new FormatException("Invalid apperance feature");
                }
            }
        }

        public Dictionary<string, Decimal> Temperament
        {
            get
            {
                return temperament;
            }

            set
            {
                if (writeOnce.isAlreadyWrite("Temperament"))
                {
                    throw new InvalidOperationException("Value already set");
                }

                temperament = value;
                writeOnce.AlreadyWrite("Temperament");
            }
        }
    }
}