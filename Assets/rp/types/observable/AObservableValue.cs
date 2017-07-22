using System;
using System.Collections.Generic;
using UnityEngine;

namespace rp.observable
{
	public abstract class AObservableValueBase<T> : AObservable, IObservableValue<T>
    {
        [SerializeField]
        protected T value = default(T);

        private ValueChange<T> _OnUpdate = null;

        public AObservableValueBase()
        {
        }

        public AObservableValueBase(T value)
        {
            this.value = value;
        }

        public AObservableValueBase(string name, object bean) : base(name, bean)
        {
        }

        public AObservableValueBase(string name, object bean, T value) : base(name, bean)
        {
            this.value = value;
        }

        public T get()
        {
            return value;
        }

        protected void _set(T value)
        {
            checkIsDisposed();
            if (EqualityComparer<T>.Default.Equals(this.value, value)) return;

            T o = this.value;
            this.value = value;
            fireChange(o, value);
        }

        protected virtual void fireChange(T oldValue, T newValue)
        {
            if (_OnUpdate != null)
            {
                List<Exception> exceptions = null;
                foreach (Delegate d in _OnUpdate.GetInvocationList())
                {
                    try
                    {
                        (d as ValueChange<T>).Invoke(this, oldValue, newValue);
                    }
                    catch (Exception e)
                    {
                        if (exceptions == null) exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                }
                if (exceptions != null)
                    throw new AggregateException("There were exceptions while firing " + name + "'s change event.", exceptions);
            }
        }

        public event ValueChange<T> OnChanged
        {
            add
            {
                checkIsDisposed();
                _OnUpdate += value;
            }
            remove
            {
                _OnUpdate -= value;
            }
        }

        protected override void DoDispose()
        {
            _OnUpdate = null;
        }
    }
}
