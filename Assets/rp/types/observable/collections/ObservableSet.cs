using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rp.observable
{
    public class ObservableSet<T> : AObservableCollection<T>, ObservableCollection<T>
    {
        private HashSet<T> set;

        public ObservableSet()
        {
            set = new HashSet<T>();
        }

        public ObservableSet(IEnumerable<T> collection)
        {
            set = new HashSet<T>(collection);
        }

        public ObservableSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            set = new HashSet<T>(collection, comparer);
        }

        public ObservableSet(IEqualityComparer<T> comparer)
        {
            set = new HashSet<T>(comparer);
        }

        #region Properties

        public int Count
        {
            get
            {
                return set.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The backing collection. included for compatibility reasons, you
        /// should not modify this.
        /// </summary>
        public HashSet<T> Data
        {
            get { return set; }
        }

        #endregion Properties

        #region Modify

        void ICollection<T>.Add(T item)
        {
            checkIsDisposed();
            if (set.Add(item) && _OnChanged != null)
            {
                change.added.Add(item);
                fireChange();
            }
        }

        public bool Add(T item)
        {
            checkIsDisposed();
            bool ret = set.Add(item);
            if (ret && _OnChanged != null)
            {
                change.added.Add(item);
                fireChange();
            }
            return ret;
        }

        public bool Remove(T item)
        {
            checkIsDisposed();
            bool ret = set.Remove(item);
            if (ret && _OnChanged != null)
            {
                change.removed.Add(item);
                fireChange();
            }
            return ret;
        }

        public int AddRange<U>(IEnumerable<U> collection) where U : T
        {
            checkIsDisposed();
            int ret = 0;
            if (_OnChanged != null)
            {
                foreach (U u in collection)
                {
                    if (set.Add(u))
                    {
                        ret++;
                        change.added.Add(u);
                    }
                }
                if (ret != 0)
                    fireChange();
            }
            else
            {
                foreach (U u in collection)
                {
                    if (set.Add(u))
                    {
                        ret++;
                    }
                }
            }
            return ret;
        }

        public int RemoveRange<U>(IEnumerable<U> collection) where U : T
        {
            checkIsDisposed();
            int ret = 0;
            if (_OnChanged != null)
            {
                foreach (U u in collection)
                {
                    if (set.Remove(u))
                    {
                        ret++;
                        change.removed.Add(u);
                    }
                }
                if (ret != 0)
                    fireChange();
            }
            else
            {
                foreach (U u in collection)
                {
                    if (set.Remove(u))
                    {
                        ret++;
                    }
                }
            }
            return ret;
        }

        public List<T> RemoveAll(Func<T, bool> func)
        {
            checkIsDisposed();
            List<T> toRemove = new List<T>();
            foreach (T u in set)
            {
                if (func(u))
                {
                    toRemove.Add(u);
                }
            }

            foreach (T u in toRemove)
                set.Remove(u);

            if (_OnChanged != null)
            {
                change.removed.AddRange(toRemove);
                fireChange();
            }
            return toRemove;
        }

        public void Clear()
        {
            checkIsDisposed();
            if (set.Count != 0)
            {
                if (_OnChanged != null)
                {
                    change.removed.AddRange(set);
                    set.Clear();
                    fireChange();
                }
                else
                {
                    set.Clear();
                }
            }
        }

        /// <summary>
        /// Adds or removes elements from the set so that it matches what's in
        /// the collection.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="collection"></param>
        public void SetAll<U>(IEnumerable<U> collection) where U : T
        {
            checkIsDisposed();
            if (_OnChanged != null)
            {
                HashSet<T> copy = new HashSet<T>(set);
                foreach (U c in collection)
                {
                    if (set.Add(c))
                    {
                        change.added.Add(c);
                    }
                    copy.Remove(c);
                }
                foreach (T c in copy)
                {
                    set.Remove(c);
                    change.removed.Add(c);
                }

                fireChange();
            }
            else
            {
                set.Clear();
                foreach (U c in collection)
                {
                    set.Add(c);
                }
            }
        }

        /// <summary>
        /// Adds or removes elements from the set so that it matches what's in
        /// the collection.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="collection"></param>
        public void SetAll(T item)
        {
            checkIsDisposed();
            if (_OnChanged != null)
            {
                if (set.Contains(item))
                {
                    if (set.Count > 1)
                    {
                        foreach (T x in set)
                        {
                            if (!set.Comparer.Equals(item, x))
                            {
                                change.removed.Add(x);
                            }
                        }
                        set.Clear();
                        set.Add(item);
                        fireChange();
                    }
                }
                else
                {
                    change.removed.AddRange(set);
                    set.Clear();
                    change.added.Add(item);
                    set.Add(item);
                    fireChange();
                }
            }
            else
            {
                set.Clear();
                set.Add(item);
            }
        }

        #endregion Modify

        #region Read-Only

        public bool Contains(T item)
        {
            return set.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            set.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return set.GetEnumerator();
        }

        #endregion Read-Only
    }
}
