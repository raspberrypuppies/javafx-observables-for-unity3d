using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace rp.observable
{
	public delegate void CollectionChangeDelegate<T>(CollectionChange<T> change);

	public interface ObservableCollection<T> : IObservable, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		event CollectionChangeDelegate<T> OnChanged;

		bool SuspendEvents
		{
			get; set;
		}

		Func<T, string> ValueToString { get; set; }
	}

	public abstract class AObservableCollection<T> : AObservable
	{
		protected CollectionChange<T> change;
		protected CollectionChangeDelegate<T> _OnChanged;
		private bool suspendEvents = false;

		public bool SuspendEvents
		{
			get
			{
				return suspendEvents;
			}
			set
			{
				if (value == suspendEvents) return;
				suspendEvents = value;
				if (!suspendEvents)
				{
					fireChange();
				}
			}
		}

		public Func<T, string> ValueToString { get; set; }

		public AObservableCollection()
		{
			change = new CollectionChange<T>((ObservableCollection<T>)this);
		}

		public AObservableCollection(String name, object bean) : base(name, bean)
		{
			change = new CollectionChange<T>((ObservableCollection<T>)this);
		}

		public event CollectionChangeDelegate<T> OnChanged
		{
			add { checkIsDisposed(); _OnChanged += value; }
			remove { _OnChanged -= value; }
		}

		protected override void DoDispose()
		{
			_OnChanged = null;
		}

		protected void fireChange()
		{
			if (suspendEvents || !change.hasChanges)
			{
				return;
			}

			if (log)
			{
				if (ValueToString != null)
				{
					Debug.Log(name + ".OnChange() " + change.ToString(ValueToString), Bean as UnityEngine.Object);
				}
				else
				{
					Debug.Log(name + ".OnChange() " + change.ToString(), Bean as UnityEngine.Object);
				}
			}

			List<Exception> exceptions = null;
			foreach (Delegate d in _OnChanged.GetInvocationList())
			{
				try
				{
					(d as CollectionChangeDelegate<T>).Invoke(change);
				}
				catch (Exception e)
				{
					if (exceptions == null) exceptions = new List<Exception>();
					exceptions.Add(e);
					Debug.LogException(e);
				}
			}
			change.Reset();
			if (exceptions != null)
				throw new AggregateException("There were exceptions while firing " + name + "'s change event.", exceptions);
		}
	}
}
