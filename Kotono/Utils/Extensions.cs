using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kotono.Utils
{
    internal static class Extensions
    {
        /// <summary>
        /// Remove the last element from a list of type TSource.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        internal static void RemoveLast<TSource>(this List<TSource> source)
        {
            if (source.Count > 0)
            {
                source.RemoveAt(source.Count - 1);
            }
        }

        /// <summary>
        /// Get an array of members with the specified attribute T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static MemberInfo[] OfAttribute<T>(this MemberInfo[] memberInfo) where T : Attribute
        {
            return memberInfo.Where(m => m.GetCustomAttribute<T>() != null).ToArray();
        }

        /// <summary>
        /// Get the type of a member.
        /// This function should only be used for <see cref="FieldInfo"/> or <see cref="PropertyInfo"/> types.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        internal static Type MemberType(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fieldInfo => fieldInfo.FieldType,
                PropertyInfo propertyInfo => propertyInfo.PropertyType,
                _ => throw new NotSupportedException("error: This function should only be used for FieldInfo or PropertyInfo types.")
            };
        }

        /// <summary>
        /// Set the value of the member supported by the given object.
        /// This function should only be used for <see cref="FieldInfo"/> or <see cref="PropertyInfo"/> types.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <exception cref="NotSupportedException"></exception>
        internal static void SetValue(this MemberInfo memberInfo, object? obj, object? value)
        {
            value = Convert.ChangeType(value, memberInfo.MemberType());

            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    fieldInfo.SetValue(obj, value);
                    break;

                case PropertyInfo propertyInfo:
                    propertyInfo.SetValue(obj, value);
                    break;

                default:
                    throw new NotSupportedException("error: This function should only be used for FieldInfo or PropertyInfo types.");
            };
        }
        
        /// <summary>
        /// Get the value of the member supported by the given object.
        /// This function should only be used for <see cref="FieldInfo"/> or <see cref="PropertyInfo"/> types.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <exception cref="NotSupportedException"></exception>
        internal static object? GetValue(this MemberInfo memberInfo, object? obj)
        {
            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    return fieldInfo.GetValue(obj);

                case PropertyInfo propertyInfo:
                    return propertyInfo.GetValue(obj);

                default:
                    throw new NotSupportedException("error: This function should only be used for FieldInfo or PropertyInfo types.");
            };
        }

        /// <summary>
        /// Get a sorted list of type TSource.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        internal static List<TSource> Sorted<TSource>(this List<TSource> source)
        {
            source.Sort();
            return source;
        }

        /// <summary>
        /// Get a sorted string given a separator and a newSeparator to replace it with.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <param name="newSeparator"></param>
        /// <returns></returns>
        internal static string Sorted(this string source, string separator, string newSeparator)
        {
            var sortedList = source.Split(separator).ToList().Sorted();

            source = "";
            foreach (var item in sortedList)
            {
                source += item + newSeparator;
            }

            return source;
        }
    }
}
