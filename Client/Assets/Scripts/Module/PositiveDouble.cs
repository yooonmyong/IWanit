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
            if (value < 0 || value > Constants.FullMotive)
            {
                throw new ArgumentException("Value is out of range.");
            }

            this.value = value;
        }

        public static implicit operator double(PositiveDouble positiveDouble)
        {
            return positiveDouble.value;
        }

        public static explicit operator PositiveDouble(double value)
        {
            if (value < 0 || value > Constants.FullMotive)
            {
                throw new ArgumentOutOfRangeException("Value is out of range.");
            }
            return new PositiveDouble(value);
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