using System;

namespace HangBreaker.Tests.Utils {
    public abstract class TestControlBase {
        public abstract object ValueCore { get; set; }

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
