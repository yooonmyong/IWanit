using System;
using System.Collections;
using System.Collections.Generic;
using Realms;
using Module;

namespace Model
{
    public class Motive : RealmObject
    {
        public Random random = new Random();

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

        public double Hygiene
        {
            get; set;
        }

        public double Urine
        {
            get; set;
        }

        public double LackMotive
        {
            get; set;
        }

        public Motive()
        {
        }

        public Motive(Guid id, double LackMotive)
        {
            this.ID = id;
            this.LackMotive = LackMotive;
            this.Fun =
                LackMotive
                + (Constants.FullMotive - LackMotive) * random.NextDouble();
            this.Energy =
                LackMotive
                + (Constants.FullMotive - LackMotive) * random.NextDouble();
            this.Hunger =
                LackMotive
                + (Constants.FullMotive - LackMotive) * random.NextDouble();
            this.Social =
                LackMotive
                + (Constants.FullMotive - LackMotive) * random.NextDouble();
            this.Stress =
                LackMotive
                + (Constants.FullMotive - LackMotive) * random.NextDouble();
            this.Hygiene =
                LackMotive
                + (Constants.FullMotive - LackMotive) * random.NextDouble();
            this.Urine =
                LackMotive
                + (Constants.FullMotive - LackMotive) * random.NextDouble();
        }
    }

    public class MotiveValue
    {
        public readonly Motive motive;

        public PositiveDouble Fun
        {
            get; set;
        }

        public PositiveDouble Energy
        {
            get; set;
        }

        public PositiveDouble Hunger
        {
            get; set;
        }

        public PositiveDouble Social
        {
            get; set;
        }

        public PositiveDouble Stress
        {
            get; set;
        }

        public PositiveDouble Hygiene
        {
            get; set;
        }

        public PositiveDouble Urine
        {
            get; set;
        }

        public void UpdateMotiveRandomly()
        {
            this.Fun -= motive.random.NextDouble() * Constants.HandlingDigit;
            this.Energy -= motive.random.NextDouble() * Constants.HandlingDigit;
            this.Hunger -= motive.random.NextDouble() * Constants.HandlingDigit;
            this.Social -= motive.random.NextDouble() * Constants.HandlingDigit;
            this.Hygiene -= motive.random.NextDouble() * Constants.HandlingDigit;
            this.Urine -= motive.random.NextDouble() * Constants.HandlingDigit;
        }

        public bool IsMotiveLack()
        {
            if 
            (
                this.Fun <= this.motive.LackMotive
                || this.Energy <= this.motive.LackMotive
                || this.Hunger <= this.motive.LackMotive
                || this.Social <= this.motive.LackMotive
                || this.Hygiene <= this.motive.LackMotive
                || this.Urine <= this.motive.LackMotive
            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MotiveValue(Motive motive)
        {
            this.motive = motive;
            this.Fun = new PositiveDouble(motive.Fun);
            this.Energy = new PositiveDouble(motive.Energy);
            this.Hunger = new PositiveDouble(motive.Hunger);
            this.Social = new PositiveDouble(motive.Social);
            this.Stress = new PositiveDouble(motive.Stress);
            this.Hygiene = new PositiveDouble(motive.Hygiene);
            this.Urine = new PositiveDouble(motive.Urine);
        }
    }
}