using System;
using System.Collections;
using System.Collections.Generic;
using Realms;

namespace Model
{
    public class Language : RealmObject
    {
        [PrimaryKey]
        public Guid ID
        {
            get; set;
        }

        public IDictionary<string, double> Expressions
        {
            get;
        }

        public Language()
        {
        }

        public Language(Guid id)
        {
            this.ID = id;
        }
    }
}