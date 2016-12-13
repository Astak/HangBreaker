using System;
namespace HangBreaker.Tests.Utils {
    public class TestControl<T> :TestControlBase {
        private T fValue;
        public T Value {
            get { return fValue; }
            set {
                if (object.Equals(fValue, value)) return;
                fValue = value;
                OnValueChanged();
            }
        }

        public override void SetValue(object value) {
            Value = (T)value;
        }
    }
}
