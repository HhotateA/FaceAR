using System;

namespace VRoidSDK.Example
{
    public class ReactivePropertyBase<T> : IDisposable
    {
        protected T _value;
        private event Action<T> PropertyChange;

        public void Subscribe(Action<T> eventHandle)
        {
            PropertyChange += eventHandle;
        }

        public void Set(T value)
        {
            _value = value;
            if (PropertyChange != null)
            {
                PropertyChange(_value);
            }
        }

        public T Get()
        {
            return _value;
        }

        public void Dispose()
        {
            PropertyChange = null;
        }
    }
}