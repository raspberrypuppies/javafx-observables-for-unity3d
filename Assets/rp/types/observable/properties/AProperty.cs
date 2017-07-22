namespace rp.observable
{
	public abstract class AProperty<T> : AObservableValueBase<T>, IProperty<T>
	{
		private IObservableValue<T> boundTo = null;

		public AProperty()
		{
		}

		public AProperty(T value) : base(value)
		{
		}

		public AProperty(string name, object bean) : base(name, bean)
		{
		}

		public AProperty(string name, object bean, T value) : base(name, bean, value)
		{
		}

		public void set(T value)
		{
			_set(value);
		}

		public bool isBound
		{
			get
			{
				return boundTo != null;
			}
		}

		private void BoundTo_OnUpdate(IObservableValue<T> v, T o, T n)
		{
			_set(n);
		}

		public void bind(IObservableValue<T> observable)
		{
			checkIsDisposed();
			/*
            if (isBound)
            {
                throw new Exception("Cannot bind to another property because " + name + " is already bound to " + boundTo.name);
            }
			*/
			if (boundTo != observable)
			{
				if (boundTo != null)
				{
					boundTo.OnChanged -= BoundTo_OnUpdate;
					boundTo = null;
				}
				boundTo = observable;
				observable.OnChanged += BoundTo_OnUpdate;
				set(boundTo.get());
			}
		}

		public void unBind()
		{
			if (boundTo != null)
			{
				boundTo.OnChanged -= BoundTo_OnUpdate;
				boundTo = null;
			}
		}

		protected override void DoDispose()
		{
			unBind();
			base.DoDispose();
		}
	}
}
