using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI
{
    internal class NullableField<T>
    {
        private T _value;

        public NullableField()
        { }

        public bool HasValue { get; set; }

        public void ResetValue()
        {
            _value = default;
            HasValue = false;
        }

        public T Value 
        {
            get { return HasValue ? _value : throw new InvalidOperationException(); }
            set { _value = value; HasValue = true; }
        }

        public T GetValueOrDefault() => HasValue ? _value : default;
    }
}
