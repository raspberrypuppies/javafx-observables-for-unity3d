using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace rp
{
	public static class CollectionX
	{
		public static V GetWithDefault<K, V>(this Dictionary<K, V> map, K key)
		{
			return GetWithDefault(map, key, default(V));
		}

		public static V GetWithDefault<K, V>(this Dictionary<K, V> map, K key, V defaultValue)
		{
			V ret;
			if (map.TryGetValue(key, out ret)) return ret;
			return defaultValue;
		}

		public static bool TryRemoveValue<K, V>(this Dictionary<K, V> map, K key, out V removedValue)
		{
			if (map.TryGetValue(key, out removedValue))
			{
				map.Remove(key);
				return true;
			}
			return false;
		}

		public static string ToStringJson<T>(this IEnumerable<T> collection)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append('[');
			foreach (T i in collection)
			{
				sb.Append(i.ToString());
				sb.Append(",");
			}
			if (sb.Length > 1)
				sb[sb.Length - 1] = ']';
			else sb.Append(']');
			return sb.ToString();
		}

		public static string ToStringJson<T>(this IEnumerable<T> collection, Func<T, string> valueToString)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append('[');
			foreach (T i in collection)
			{
				sb.Append(valueToString(i));
				sb.Append(",");
			}
			if (sb.Length > 1)
				sb[sb.Length - 1] = ']';
			else sb.Append(']');
			return sb.ToString();
		}

		public static StringBuilder AppendCollection<T>(this StringBuilder sb, IEnumerable<T> collection)
		{
			bool didSomething = false;
			sb.Append('[');
			foreach (T i in collection)
			{
				sb.Append(i.ToString());
				sb.Append(",");
				didSomething = true;
			}
			if (didSomething)
				sb[sb.Length - 1] = ']';
			else sb.Append(']');
			return sb;
		}

		public static StringBuilder AppendCollection<T>(this StringBuilder sb, IEnumerable<T> collection, Func<T, string> valueToString)
		{
			bool didSomething = false;
			sb.Append('[');
			foreach (T i in collection)
			{
				sb.Append(valueToString(i));
				sb.Append(",");
				didSomething = true;
			}
			if (didSomething)
				sb[sb.Length - 1] = ']';
			else sb.Append(']');
			return sb;
		}

		public static int AddRange<T, D>(this HashSet<T> set, IEnumerable<D> toAdd) where D : T
		{
			int added = 0;
			foreach (D item in toAdd)
			{
				if (set.Add(item))
					added++;
			}
			return added;
		}

		public static int RemoveRange<T, D>(this HashSet<T> set, IEnumerable<D> toRemove) where D : T
		{
			int removed = 0;
			foreach (D item in toRemove)
			{
				if (set.Remove(item))
					removed++;
			}
			return removed;
		}

		/// <summary>
		/// Adds the toAdd collection to the dictionary. Any values with
		/// duplicate keys will be overridden.
		/// </summary>
		/// <typeparam name="K">Key type</typeparam>
		/// <typeparam name="V">Value type</typeparam>
		/// <typeparam name="D">Class derived from V</typeparam>
		/// <param name="dictionary">The dictionary items are added to</param>
		/// <param name="toAdd">The collection of items to add</param>
		/// <param name="keySelector">The function that selects the key</param>
		public static void AddRangeOverwrite<K, V, D>(this Dictionary<K, V> dictionary, IEnumerable<D> toAdd, System.Func<D, K> keySelector) where D : V
		{
			foreach (D item in toAdd)
			{
				dictionary[keySelector(item)] = item;
			}
		}
	}
}
