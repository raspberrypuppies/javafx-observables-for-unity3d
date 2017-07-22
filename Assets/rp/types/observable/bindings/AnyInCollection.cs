using System;
using System.Collections.Generic;

namespace rp.observable
{
	/// <summary>
	/// Checks a collection if a predicate holds true for ANY of the items.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AnyInCollection<T> : AObservableValueBase<bool>
	{
		ObservableCollection<T> collection;
		protected bool isEnabled = false;

		Predicate<T> elementPredicate;
		Predicate<ICollection<T>> collectionPredicate;

		/// <summary>
		/// The collection we're watching
		/// </summary>
		public ObservableCollection<T> Collection
		{
			get
			{
				return collection;
			}
			set
			{
				checkIsDisposed();
				if (collection == value) return;

				if (isEnabled)
				{
					if (collection != null)
					{
						collection.OnChanged -= Collection_OnChanged;
					}
					collection = value;
					if (collection != null)
					{
						collection.OnChanged += Collection_OnChanged;
						_set(Calculate());
					}
					else
					{
						_set(false);
					}
				}
				else
				{
					collection = value;
				}
			}
		}

		/// <summary>
		/// Are we watching for changes? Set to false to turn off dynamic recalculation.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return isEnabled;
			}
			set
			{
				checkIsDisposed();
				if (isEnabled == value) return;

				this.isEnabled = value;
				if (isEnabled)
				{
					if (collection != null)
					{
						collection.OnChanged += Collection_OnChanged;
						_set(Calculate());
					}
					else
					{
						_set(false);
					}
				}
				else
				{
					if (collection != null)
					{
						collection.OnChanged -= Collection_OnChanged;
					}
				}
			}
		}
		/// <summary>
		/// If any elements of the collection meet this predicate, then the observable takes the value of true.
		/// </summary>
		public Predicate<T> ElementPredicate
		{
			get
			{
				return elementPredicate;
			}
			set
			{
				checkIsDisposed();
				if (elementPredicate == value) return;

				elementPredicate = value;

				if (isEnabled)
				{
					_set(Calculate());
				}
			}
		}
		/// <summary>
		/// This predicate checks the whole collection for some condition. If it returns true, then the Binding will be true.
		/// </summary>
		public Predicate<ICollection<T>> CollectionPredicate
		{
			get
			{
				return collectionPredicate;
			}
			set
			{
				checkIsDisposed();
				if (collectionPredicate == value) return;

				collectionPredicate = value;

				if (isEnabled)
				{
					_set(Calculate());
				}
			}
		}

		public AnyInCollection()
		{
		}

		public AnyInCollection(ObservableCollection<T> collection)
		{
			this.Collection = collection;
		}

		/// <summary>
		/// Binding will be true so long as the collection is not empty
		/// </summary>
		/// <param name="name"></param>
		/// <param name="bean"></param>
		/// <param name="collection"></param>
		public AnyInCollection(string name, object bean, ObservableCollection<T> collection) : base(name, bean)
		{
			this.Collection = collection;
		}

		public AnyInCollection(string name, object bean, ObservableCollection<T> collection, Predicate<T> elementPredicate, Predicate<ICollection<T>> collectionPredicate) : base(name, bean)
		{
			this.ElementPredicate = elementPredicate;
			this.CollectionPredicate = collectionPredicate;
			this.Collection = collection;
		}

		public AnyInCollection(string name, object bean) : base(name, bean)
		{
		}

		private void Collection_OnChanged(CollectionChange<T> change)
		{
			//if we're true and they removed an item
			//or we're false and they added an item
			bool weAreTrue = get();
			if ((weAreTrue && change.removed.Count != 0) || (!weAreTrue && change.added.Count != 0))
			{
				//then recalculate
				_set(Calculate());
			}
		}
		
		/// <summary>
		/// Forces the value to update
		/// </summary>
		public void Recalculate()
		{
			checkIsDisposed();
			_set(Calculate());
		}

		protected override void DoDispose()
		{
			base.DoDispose();
			isEnabled = false;
			collectionPredicate = null;
			elementPredicate = null;
		}
		
		/// <summary>
		/// Rechecks the whole collection to make sure conditions are met.
		/// TODO: Create a version that only checkes changed elements.
		/// </summary>
		/// <returns></returns>
		protected bool Calculate()
		{
			if (elementPredicate == null && collectionPredicate == null)
				return collection.Count > 0;

			if (elementPredicate != null)
			{
				foreach (T value in Collection)
					if (elementPredicate(value))
						return true;
			}

			if (collectionPredicate != null)
			{
				if (collectionPredicate(collection))
					return true;
			}

			return false;
		}
	}
}
