using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpeedifyCliWrapper.Common
{
    internal class SpeedifyCollection<T> : SpeedifyReturnedValue, IList<T> where T : SpeedifyReturnedValue
    {
        private Speedify _lWrapper;
        private List<T> _storage;

        internal override Speedify _wrapper
        {
            get => this._lWrapper;
            set
            {
                this._lWrapper = value;
                foreach (T toChange in this._storage.Where(i => i._wrapper == null))
                {
                    toChange._wrapper = this._lWrapper;
                }
            }
        }

        public SpeedifyCollection()
        {
            this._storage = new List<T>();
        }

        public SpeedifyCollection(Speedify wrapper) : base()
        {
            this._wrapper = wrapper;
        }

        private void CheckAndAddWrapper(T item)
        {
            if (item._wrapper == null)
            {
                item._wrapper = this._wrapper;
            }
        }

        public T this[int index] { get => ((IList<T>)_storage)[index]; set => ((IList<T>)_storage)[index] = value; }

        public int Count => ((IList<T>)_storage).Count;

        public bool IsReadOnly => ((IList<T>)_storage).IsReadOnly;


        public void Add(T item)
        {
            this.CheckAndAddWrapper(item);

            ((IList<T>)_storage).Add(item);
        }

        public void Clear()
        {
            ((IList<T>)_storage).Clear();
        }

        public bool Contains(T item)
        {
            return ((IList<T>)_storage).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((IList<T>)_storage).CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IList<T>)_storage).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)_storage).IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.CheckAndAddWrapper(item);

            ((IList<T>)_storage).Insert(index, item);
        }

        public bool Remove(T item)
        {
            return ((IList<T>)_storage).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)_storage).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<T>)_storage).GetEnumerator();
        }

        public static implicit operator List<T>(SpeedifyCollection<T> collection)
        {
            return collection._storage;
        }
    }
}
