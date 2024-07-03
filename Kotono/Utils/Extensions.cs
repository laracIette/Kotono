using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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
        /// Get an array of members with the attribute of type T.
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
            return memberInfo switch
            {
                FieldInfo fieldInfo => fieldInfo.GetValue(obj),
                PropertyInfo propertyInfo => propertyInfo.GetValue(obj),
                _ => throw new NotSupportedException("error: This function should only be used for FieldInfo or PropertyInfo types."),
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

        /// <summary>
        /// Get a sorted string given a separator.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        internal static string Sorted(this string source, string separator)
        {
            return source.Sorted(separator, separator);
        }

        /// <summary>
        /// Get wether a Type is a list. If <see langword="true"/>, itemType gets assigned the type the list contains.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        internal static bool IsGenericList(this Type type, out Type itemType)
        {
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>)))
            {
                itemType = type.GetGenericArguments()[0];
                return true;
            }

            itemType = type;
            return false;
        }

        /// <summary>
        /// Get a string that represents the current IEnumerable's content.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        internal static string ToStringContent<TSource>(this IEnumerable<TSource> source)
        {
            if (!source.Any())
            {
                return "[]";
            }

            string result = "[";

            foreach (var item in source)
            {
                result += item + ", ";
            }

            return result[..^2] + "]";
        }

        internal static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source) where TSource : class
        {
            return source.Any() ? source.First() : null;
        }

        internal static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) where TSource : class
        {
            return source.Any(predicate) ? source.First(predicate) : null;
        }

        /// <summary>
        /// Get all the methods of a type excluding static methods.
        /// </summary>
        /// <param name="type"> The type to get the methods from. </param>
        internal static MethodInfo[] GetAllMethods(this Type type)
        {
            return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Get all the methods of a type excluding static methods, given a predicate.
        /// </summary>
        /// <param name="type"> The type to get the methods from. </param>
        /// <param name="predicate"> The predicate. </param>
        internal static IEnumerable<MethodInfo> GetAllMethods(this Type type, Func<MethodInfo, bool> predicate)
        {
            return type.GetAllMethods().Where(predicate);
        }

        /// <summary>
        /// Adds an item to the List if it doesn't contain the item.
        /// </summary>
        /// <returns> Wether the item was added to the List. </returns>
        internal static bool TryAddUnique<TSource>(this List<TSource> source, TSource item)
        {
            if (source.Contains(item))
            {
                return false;
            }
            else
            {
                source.Add(item);
                return true;
            }
        }

        /// <summary>
        /// Get wether the object is of type T.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool OfType<T>(this object? obj)
        {
            return obj?.GetType() == typeof(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static string Keep(this string source, string other)
        {
            return new string(source.Where(other.Contains).ToArray());
        }
    }
}
