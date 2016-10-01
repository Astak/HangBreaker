using HangBreaker.ViewModels;
using System.Drawing;
using System.Windows.Forms;

namespace HangBreaker {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            InitializeBindings();
        }

        protected override void OnLoad(System.EventArgs e) {
            base.OnLoad(e);
            SetPosition();
        }

        private void InitializeBindings() {
            if (DesignMode) return;
            var api = MvvmContext.OfType<MainViewModel>();
            api.SetBinding(this, d => d.Opacity, vm => vm.IsTransparent, val => val ? .5 : 1.0);
        }

        private void SetPosition() {
            Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Right - Width - 50,
                50
                );
        }
    }
}
