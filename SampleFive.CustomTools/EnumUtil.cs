using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SampleFive.CustomTools
{
    public static class EnumUtil
    {


        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Parse a string value to the given Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            Debug.Assert(!string.IsNullOrEmpty(value));
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Converts Enumeration type into a dictionary of names and values
        /// </summary>
        /// <param name="t">Enum type</param>
        public static IDictionary<string, int> EnumToDictionary(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.GetTypeInfo().IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return (from i in Enumerable.Range(0, names.Length)
                    select new { Key = names[i], Value = (int)values.GetValue(i) })
                .ToDictionary(k => k.Key, k => k.Value);
        }


        public static class EnumHelper<T>
        {
            /// <summary>
            /// 
            /// Example:
            /// public enum EnumGrades
            ///{
            /// [Description("Passed")]
            /// Pass,
            /// [Description("Failed")]
            /// Failed,
            /// [Description("Promoted")]
            /// Promoted
            ///}
            ///
            /// string description = EnumHelper<![CDATA[<EnumGrades>]]>.GetEnumDescription("pass");
            /// </summary>
            /// <typeparam name="T">Enum Type</typeparam>
            public static string GetEnumDescription(string value)
            {
                Type type = typeof(T);
                var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

                if (name == null)
                {
                    return string.Empty;
                }
                var field = type.GetField(name);
                var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return customAttribute.Any() ? ((DescriptionAttribute)customAttribute.FirstOrDefault()).Description : name;
            }
        }
    }
}
