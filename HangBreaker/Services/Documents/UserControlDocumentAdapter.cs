using DevExpress.Utils.MVVM.Services;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HangBreaker.Documents {
    public sealed class UserControlDocumentAdapter :IDocumentAdapter {
        private Form form;

        public UserControlDocumentAdapter(Form form) {
            this.form = form;
        }
        #region IDocumentAdapter
        void IDocumentAdapter.Close(Control control, bool force) {
            if (!RaiseClosing()) return; 
            control.Dispose();
            if (form.Controls.Count == 0) form.Close();
            RaiseClosed();
        }

        private EventHandler fClosed;
        event EventHandler IDocumentAdapter.Closed {
            add { fClosed += value; }
            remove { fClosed -= value; }
        }
        private void RaiseClosed() {
            fClosed?.Invoke(this, EventArgs.Empty);
        }

        private CancelEventHandler fClosing;
        event CancelEventHandler IDocumentAdapter.Closing {
            add { fClosing += value; }
            remove { fClosing -= value ; }
        }
        private bool RaiseClosing() {
            var args = new CancelEventArgs();
            fClosing?.Invoke(this, args);
            return !args.Cancel;
        }

        void IDocumentAdapter.Show(Control control) {
            control.Dock = DockStyle.Fill;
            control.Parent = form;
            control.BringToFront();
        }

        void IDisposable.Dispose() { }
        #endregion
    }
}
