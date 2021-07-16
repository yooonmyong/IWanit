using System;
using System.Reflection;

namespace Module 
{
    public static class EnumToStringConverter 
    { 
        public static string GetStringValue(Errorcase value) 
        { 
            string output = null; 
            Type type = value.GetType(); 
            FieldInfo fi = type.GetField(value.ToString()); 
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[]; 
            if (attrs.Length > 0) 
            { 
                output = attrs[0].Value; 
            } 
            
            return output; 
        } 
    }
}