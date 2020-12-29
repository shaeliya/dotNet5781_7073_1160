using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    static class Cloning
    {
        /// <summary>
        /// Makes a generic shallowed copy, properties only
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        internal static T Clone<T>(this T original) where T : new()
        {
            T clone = new T();

            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                propertyInfo.SetValue(clone, propertyInfo.GetValue(original, null), null);
            }

            return clone;
        }
    }
}
