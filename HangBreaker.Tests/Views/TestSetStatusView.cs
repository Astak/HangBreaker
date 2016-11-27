using DevExpress.Utils.MVVM;
using HangBreaker.BusinessModel;
using HangBreaker.Tests.Utils;
using HangBreaker.ViewModels;

namespace HangBreaker.Tests.Views {
    public class TestSetStatusView {
        private readonly MVVMContext Context;
        public readonly TestAction OKAction;
        public readonly TestControl<WorkSessionStatus> StatusControl;

        public TestSetStatusView() {
            Context = new MVVMContext();
            Context.ViewModelType = typeof(SetStatusViewModel);
            OKAction = new TestAction();
            StatusControl = new TestControl<WorkSessionStatus>();
            var api = Context.OfType<SetStatusViewModel>();
            api.BindCommand(OKAction, vm => vm.Ok());
            api.SetBinding(StatusControl, ctrl => ctrl.Value, vm => vm.Status);
        }
    }
}
