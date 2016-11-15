using System;
namespace HangBreaker.Tests.Utils {
    public class TestControl<T> {
        private T fValue;
        public T Value {
            get { return fValue; }
            set {
                if (object.Equals(fValue, value)) return;
                fValue = value;
                OnValueChanged();
            }
        }

        protected void OnValueChanged() {
            if (fValueChanged != null)
                fValueChanged(this, EventArgs.Empty);
        }

        private EventHandler fValueChanged;
        public event EventHandler ValueChanged {
            add { fValueChanged += value; }
            remove { fValueChanged -= value; }
        }
    }
}
