using System;
using System.Collections;
using System.Collections.Generic;
using Realms;
using Module;

namespace Model
{
    public class Language : RealmObject
    {
        [PrimaryKey]
        public Guid ID
        {
            get; set;
        }

        public int StandardtoRememberWord
        {
            get; set;
        }

        public string NotyetRememberedWords
        {
            get; set;
        }

        public string RememberedWords
        {
            get; set;
        }

        public Language()
        {
        }

        public Language(Guid ID)
        {
            var random = new System.Random();

            this.ID = ID;
            this.StandardtoRememberWord = 
                random.Next
                (
                    Constants.MinStandardtoRememberWord, 
                    Constants.MaxStandardtoRememberWord
                );
            this.NotyetRememberedWords = "{}";
            this.RememberedWords = "{}";
        }
    }
}