using System;
using System.Collections;
using System.Collections.Generic;
using Realms;

namespace Model
{
    public class Motive : RealmObject
    {
        public readonly Decimal LACK;
        private const double FULL = 10.0f;

        [PrimaryKey]
        public Guid ID
        {
            get; set;
        }

        public double Fun
        {
            get; set;
        }

        public double Energy
        {
            get; set;
        }

        public double Hunger
        {
            get; set;
        }

        public double Social
        {
            get; set;
        }

        public double Stress
        {
            get; set;
        }

        public Motive()
        {
        }

        public Motive(Guid id, Decimal intensity)
        {
            this.ID = id;
            this.Fun = FULL;
            this.Energy = FULL;
            this.Hunger = FULL;
            this.Social = FULL;
            this.Stress = FULL;
        }
    }
}