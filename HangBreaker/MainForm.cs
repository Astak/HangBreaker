using DevExpress.Mvvm;
using DevExpress.Utils.MVVM.Services;
using HangBreaker.Documents;
using HangBreaker.Services;
using HangBreaker.Utils;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace HangBreaker {
    public sealed partial class MainForm : Form, IDocumentAdapterFactory {
        public MainForm() {
            InitializeComponent();
            ServiceContainer.Default.RegisterService(new XpoService("HangBreaker"));
            var service = (IDocumentManagerService)DocumentManagerService.Create(this);
            ServiceContainer.Default.RegisterService(Constants.ServiceKey, service);
            IDocument document = service.CreateDocument(Constants.MainViewName, null, null);
            document.Show();
        }

        protected override void OnLoad(System.EventArgs e) {
            base.OnLoad(e);
            SetPosition();
        }

        private void SetPosition() {
            Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Right - Width - 20,
                20
                );
        }

        #region IDocumentAdapterFactory
        IDocumentAdapter IDocumentAdapterFactory.Create() => new UserControlDocumentAdapter(this);
        #endregion
    }
}
