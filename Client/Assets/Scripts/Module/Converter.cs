using System;
using System.Collections;
using System.Collections.Generic;

namespace Module
{
    public static class Converter<T>
    {
        public static string ConvertDictionaryToJson
        (
            Dictionary<string, T> input
        )
        {
            string output = "{";

            foreach (string key in input.Keys)
            {
                output += "\"";
                output += key;
                output += "\": ";
                output += input[key];
                output += ", ";
            }

            output = output.Remove(output.Length - 2, 2);
            output += "}";

            return output;
        }

        public static double ConvertBoolToDouble(bool input)
        {
            if (input)
            {
                return 1.0;
            }
            else
            {
                return -1.0;
            }
        }
    }
}