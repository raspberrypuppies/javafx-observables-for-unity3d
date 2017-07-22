using System;
using System.Collections.Generic;
using System.Text;

namespace rp.observable
{
	/// <summary>
	/// represents a simple collection change. These should not be held on to
	/// after the change event is fired as they are probably reused between calls.
	/// </summary>
	public class CollectionChange<T>
	{
		/// <summary>
		/// The collection that changed
		/// </summary>
		public readonly ObservableCollection<T> collection = null;

		public readonly List<T> added = new List<T>();
		public readonly List<T> removed = new List<T>();
		public bool wasPermutationChange = false;

		public CollectionChange(ObservableCollection<T> collection)
		{
			this.collection = collection;
		}

		public bool hasChanges
		{
			get
			{ return added.Count != 0 || removed.Count != 0 || wasPermutationChange; }
		}

		public void Reset()
		{
			added.Clear(); removed.Clear(); wasPermutationChange = false;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (added.Count != 0)
				sb.Append(" Added:=").AppendCollection(added);
			if (removed.Count != 0)
				sb.Append(" Removed:=").AppendCollection(removed);
			if (wasPermutationChange)
				sb.Append(" (Was Reordered)");
			return sb.ToString();
		}

		public string ToString(Func<T, string> valueToString)
		{
			StringBuilder sb = new StringBuilder();
			if (added.Count != 0)
			{
				sb.Append(" Added:=").AppendCollection(added, valueToString);
				if (removed.Count != 0)
					sb.Append("\n");
			}
			if (removed.Count != 0)
				sb.Append(" Removed:=").AppendCollection(removed, valueToString);
			if (wasPermutationChange)
				sb.Append(" (Was Reordered)");
			return sb.ToString();
		}
	}
}
