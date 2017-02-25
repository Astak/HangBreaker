using DevExpress.Utils.MVVM.UI;
using HangBreaker.BusinessModel;
using HangBreaker.ViewModels;
using System;
using System.Windows.Forms;

namespace HangBreaker.Views {
    [ViewType(HangBreaker.Utils.Constants.SetStatusViewName)]
    public partial class SetStatusView : UserControl {
        public SetStatusView() {
            InitializeComponent();
            foreach (WorkSessionStatus status in Enum.GetValues(typeof(WorkSessionStatus)))
                cbStatus.Items.Add(status);
            var api = Context.OfType<SetStatusViewModel>();
            api.SetBinding(cbStatus, cb => cb.SelectedIndex, vm => vm.Status, 
                val => val == null ? -1 : cbStatus.Items.IndexOf(val), 
                val => (WorkSessionStatus?)cbStatus.Items[val]);
        }
    }
}
