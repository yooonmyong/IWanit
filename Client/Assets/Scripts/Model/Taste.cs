using System;
using System.Collections;
using System.Collections.Generic;
using Realms;

namespace Model
{
    public class Taste : RealmObject
    {
        [PrimaryKey]
        public Guid ID
        {
            get; set;
        }

        public IDictionary<string, double> Food
        {
            get;
        }

        public IDictionary<string, double> Playing
        {
            get;
        }

        public IDictionary<string, double> Style
        {
            get;
        }

        public Taste()
        {
        }

        public Taste(Guid id)
        {
            this.ID = id;
        }
    }
}