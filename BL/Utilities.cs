using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Utilities
    {

    #region Deep Copy Utilities
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }
        public static object CopyPropertiesToNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type); // new object of Type
            from.CopyPropertiesTo(to);
            return to;
        }

        /// <summary>
        /// toUnion - פונקציית הרחבה שמחזירה משתנה מסוג חדש ומעתיקה את השדות מ
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="from"></param>
        /// <param name="type"></param>
        /// <param name="toUnion"></param>
        /// <returns></returns>
        public static object CopyPropertiesToNewAndUnion<S, T>(this S from, Type type, T toUnion)
        {
            var to = from.CopyPropertiesToNew(type);
            toUnion.CopyPropertiesTo(to);
            return to;
        }
        #endregion Deep Copy Utilities

        /// <summary>
        /// IEnumerable-מחזיר זוגות צמודים מ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IEnumerable<(T, T)> Pairwise<T>(IEnumerable<T> collection)
        {
            using (var enumerator = collection.GetEnumerator())
            {
                enumerator.MoveNext();
                var previous = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    yield return (previous, enumerator.Current);
                    previous = enumerator.Current;
                }
            }
        }
    }
}