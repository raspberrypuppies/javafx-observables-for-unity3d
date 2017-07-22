using System;
using System.Collections;
using System.Collections.Generic;

namespace rp.observable
{
    [Serializable]
    public class ObservableList<T> : AObservableCollection<T>, ObservableCollection<T>, IList<T>
    {
        [UnityEngine.SerializeField]
        private List<T> data;

        public ObservableList()
        {
            data = new List<T>();
        }

        public ObservableList(IEnumerable<T> t)
        {
            data = new List<T>(t);
        }

        public ObservableList(int capacity)
        {
            data = new List<T>(capacity);
        }

        /// <summary>
        /// The backing collection. included for compatibility reasons, you
        /// should not modify this.
        /// </summary>
        public List<T> Data { get { return data; } }

        #region readers

        public int Count
        {
            get { return data.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public int BinarySearch(T item)
        {
            return data.BinarySearch(item);
        }

        public int BinarySearch(T item, IComparer<T> comp)
        {
            return data.BinarySearch(item, comp);
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comp)
        {
            return data.BinarySearch(index, count, item, comp);
        }

        public bool Contains(T item)
        {
            return data.Contains(item);
        }

        public List<Tout> ConvertAll<Tout>(Converter<T, Tout> converter)
        {
            return data.ConvertAll<Tout>(converter);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public bool Exists(Predicate<T> match)
        {
            return data.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            return data.Find(match);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return data.FindAll(match);
        }

        public int FindIndex(Predicate<T> match)
        {
            return data.FindIndex(match);
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return data.FindIndex(startIndex, match);
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return data.FindIndex(startIndex, count, match);
        }

        public void ForEach(Action<T> action)
        {
            data.ForEach(action);
        }

        public override int GetHashCode()
        {
            return data.GetHashCode();
        }

        public List<T> GetRange(int index, int count)
        {
            return data.GetRange(index, count);
        }

        public int IndexOf(T item)
        {
            return data.IndexOf(item);
        }

        public int IndexOf(T item, int index)
        {
            return data.IndexOf(item, index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return data.IndexOf(item, index, count);
        }

        public int LastIndexOf(T item)
        {
            return data.LastIndexOf(item);
        }

        public int LastIndexOf(T item, int index)
        {
            return data.LastIndexOf(item, index);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            return data.LastIndexOf(item, index, count);
        }

        public T[] ToArray()
        {
            return data.ToArray();
        }

        public override string ToString()
        {
            return data.ToString();
        }

        public void TrimExcess()
        {
            data.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return data.TrueForAll(match);
        }

        #endregion readers

        #region modifiers

        public T this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                checkIsDisposed();
                if (EqualityComparer<T>.Default.Equals(data[index], value))
                    return;
                if (_OnChanged != null)
                {
                    change.removed.Add(data[index]);
                    change.added.Add(value);
                    data[index] = value;
                    fireChange();
                }
                else
                {
                    data[index] = value;
                }
            }
        }

        public void Add(T item)
        {
            checkIsDisposed();
            data.Add(item);
            if (_OnChanged != null)
            {
                change.added.Add(item);
                fireChange();
            }
        }

        public void AddRange(IEnumerable<T> source)
        {
            checkIsDisposed();
            data.AddRange(source);
            if (_OnChanged != null)
            {
                change.added.AddRange(source);
                fireChange();
            }
        }

        public void Clear()
        {
            checkIsDisposed();
            if (data.Count == 0) return;
            if (_OnChanged != null)
            {
                change.removed.AddRange(data);
                data.Clear();
                fireChange();
            }
            else
            {
                data.Clear();
            }
        }

        public void Insert(int index, T item)
        {
            checkIsDisposed();
            data.Insert(index, item);
            if (_OnChanged != null)
            {
                change.added.Add(item);
                fireChange();
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            checkIsDisposed();
            data.InsertRange(index, collection);
            if (_OnChanged != null)
            {
                change.added.AddRange(collection);
                fireChange();
            }
        }

        public bool Remove(T item)
        {
            checkIsDisposed();
            bool ret = data.Remove(item);
            if (ret && _OnChanged != null)
            {
                change.removed.Add(item);
                fireChange();
            }
            return ret;
        }

        public void RemoveAt(int index)
        {
            checkIsDisposed();
            if (_OnChanged != null)
            {
                T item = data[index];
                data.RemoveAt(index);
                change.removed.Add(item);
                fireChange();
            }
            else
            {
                data.RemoveAt(index);
            }
        }

        public int RemoveAll(Predicate<T> match)
        {
            checkIsDisposed();
            if (_OnChanged != null)
            {
                int ret = 0;
                for (int i = data.Count - 1; i != -1; i--)
                {
                    T item = data[i];
                    if (match.Invoke(item))
                    {
                        change.removed.Add(item);
                        ret++;
                        data.RemoveAt(i);
                    }
                }
                fireChange();
                return ret;
            }
            else
            {
                return data.RemoveAll(match);
            }
        }

        public void RemoveRange(int index, int count)
        {
            checkIsDisposed();
            if (_OnChanged != null)
            {
                for (int i = 0; i < count; i++)
                {
                    change.removed.Add(data[i]);
                }
                data.RemoveRange(index, count);
                fireChange();
            }
            else
            {
                data.RemoveRange(index, count);
            }
        }

        public void Reverse()
        {
            checkIsDisposed();
            data.Reverse();
            if (_OnChanged != null)
            {
                change.wasPermutationChange = true;
                fireChange();
            }
        }

        public void Reverse(int index, int count)
        {
            checkIsDisposed();
            data.Reverse(index, count);
            if (_OnChanged != null)
            {
                change.wasPermutationChange = true;
                fireChange();
            }
        }

        public void Sort()
        {
            checkIsDisposed();
            data.Sort();
            if (_OnChanged != null)
            {
                change.wasPermutationChange = true;
                fireChange();
            }
        }

        public void Sort(Comparison<T> comp)
        {
            checkIsDisposed();
            data.Sort(comp);
            if (_OnChanged != null)
            {
                change.wasPermutationChange = true;
                fireChange();
            }
        }

        public void Sort(int index, int count, IComparer<T> comp)
        {
            checkIsDisposed();
            data.Sort(index, count, comp);
            if (_OnChanged != null)
            {
                change.wasPermutationChange = true;
                fireChange();
            }
        }

        #endregion modifiers
    }
}
