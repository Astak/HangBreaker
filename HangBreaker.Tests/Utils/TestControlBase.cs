using System;

namespace HangBreaker.Tests.Utils {
    public abstract class TestControlBase {
        public abstract void SetValue(object value);

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
