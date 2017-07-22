#if UNITY_EDITOR
#define EXCEPTION_MODIFY_DESTROYED
#endif

namespace rp.observable
{
	public abstract class AObservable : IObservable
	{
		public bool IsDisposed { get; private set; }

		public AObservable()
		{
			name = GetType().Name;
		}

		public AObservable(string name, object bean)
		{
			this.name = name;
			this.Bean = bean;
		}

		/// <summary>
		/// Returns the Object that contains this property. If this property is
		/// not contained in an Object, null is returned.
		/// </summary>
		public object Bean
		{
			get; set;
		}

		/// <summary>
		/// is logging enabled for this object?
		/// </summary>
		public bool log
		{
			get; set;
		}

		/// <summary>
		/// The name of the observable object, it defaults to the class name if
		/// nothing is defined
		/// </summary>
		public string name
		{
			get; set;
		}

		protected void checkIsDisposed()
		{
#if EXCEPTION_MODIFY_DESTROYED
			if (IsDisposed)
				throw new System.ObjectDisposedException(name);
#endif
		}

		public void Dispose()
		{
			if (IsDisposed)
			{
				throw new System.ObjectDisposedException(name, "Cannot dispose an object twice");
			}
			else
			{
				IsDisposed = true;
				DoDispose();
			}
		}

		protected abstract void DoDispose();
	}
}
