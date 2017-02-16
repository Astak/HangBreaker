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
            throw new System.NotImplementedException();
        }

        event EventHandler IDocumentAdapter.Closed {
            add { throw new System.NotImplementedException(); }
            remove { throw new System.NotImplementedException(); }
        }

        event CancelEventHandler IDocumentAdapter.Closing {
            add { throw new System.NotImplementedException(); }
            remove { throw new System.NotImplementedException(); }
        }

        void IDocumentAdapter.Show(Control control) {
            control.Dock = DockStyle.Fill;
            control.Parent = form;
        }

        void IDisposable.Dispose() {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
