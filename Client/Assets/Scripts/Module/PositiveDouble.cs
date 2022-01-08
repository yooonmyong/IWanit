using System;
using System.Collections;
using Module;

namespace Module
{
    public struct PositiveDouble
    {
        private double value;

        public PositiveDouble(double value)
        {
            if (value < 0)
            {
                this.value = 0;
            }
            else if (value > Constants.FullMotive)
            {
                this.value = Constants.FullMotive;
            }
            else
            {
                this.value = value;
            }
        }

        public static implicit operator double(PositiveDouble positiveDouble)
        {
            return positiveDouble.value;
        }

        public static explicit operator PositiveDouble(double value)
        {
            if (value < 0)
            {
                return new PositiveDouble(0);
            }
            else if (value > Constants.FullMotive)
            {
                return new PositiveDouble(Constants.FullMotive);
            }
            else
            {
                return new PositiveDouble(value);
            }
        }

        public static PositiveDouble operator +
        (
            PositiveDouble first, double second
        ) 
            => new PositiveDouble(first.value + second);

        public static PositiveDouble operator -
        (
            PositiveDouble first, double second
        ) 
            => new PositiveDouble(first.value - second);

        public static PositiveDouble operator *
        (
            PositiveDouble first, double second
        )
            => new PositiveDouble(first.value * second);

        public static PositiveDouble operator /
        (
            PositiveDouble first, double second
        )
            => new PositiveDouble(first.value / second);
    }
}