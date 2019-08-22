using System;
using System.Collections;
using System.Collections.Generic;

namespace VRoidSDK.Example
{
    public class ReactiveListProperty<T> : IEnumerable<T>, IDisposable
    {
        private List<T> _value;
        private event Action<T> AddPropertyChange;
        private event Action<T> RemovePropertyChange;

        public ReactiveListProperty()
        {
            _value = new List<T>();
        }
        
        public void SubscribeAddCollection(Action<T> eventHandle)
        {
            AddPropertyChange += eventHandle;
        }

        public void SubscribeRemoveCollection(Action<T> eventHandle)
        {
            RemovePropertyChange += eventHandle;
        }

        public void Add(T value)
        {
            _value.Add(value);
            if (AddPropertyChange != null)
            {
                AddPropertyChange(value);
            }
        }

        public bool Remove(T value)
        {
            var result = _value.Remove(value);
            if (RemovePropertyChange != null)
            {
                RemovePropertyChange(value);
            }

            return result;
        }

        public bool RemoveAt(int n)
        {
            var value = _value[n];
            _value.RemoveAt(n);
            if (RemovePropertyChange != null)
            {
                RemovePropertyChange(value);
            }

            return true;  
        }

        public int Count()
        {
            return _value.Count;
        }

        public void Dispose()
        {
            AddPropertyChange = null;
            RemovePropertyChange = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _value.GetEnumerator();
        }
    }
}