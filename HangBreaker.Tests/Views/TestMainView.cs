using DevExpress.Utils.MVVM;
using HangBreaker.Tests.Utils;
using HangBreaker.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests.Views {
    public class TestMainView :TestBaseView {
        public const string StartActionName = "Start";
        public const string RestartActionName = "Restart";
        public const string TimerActionName = "Timer";
        public const string CloseActionName = "Close";
        public const string DisplayEditorName = "Display";
        public const string OpacityEditorName = "Opacity";

        public TestMainView() {
            Context.ViewModelType = typeof(MainViewModel);
            var StartAction = new TestAction();
            var RestartAction = new TestAction();
            var TimerAction = new TestAction();
            var CloseAction = new TestAction();
            var DisplayControl = new TestControl<string>();
            var OpacityControl = new TestControl<bool>();
            var api = Context.OfType<MainViewModel>();
            api.BindCommand(StartAction, vm => vm.Start());
            api.BindCommand(RestartAction, vm => vm.Restart());
            api.BindCommand(TimerAction, vm => vm.Tick());
            api.BindCommand(CloseAction, vm => vm.Close());
            api.SetBinding(DisplayControl, ctrl => ctrl.Value, vm => vm.DisplayText);
            api.SetBinding(OpacityControl, ctrl => ctrl.Value, vm => vm.IsTransparent);
            AddAction(StartActionName, StartAction);
            AddAction(RestartActionName, RestartAction);
            AddAction(TimerActionName, TimerAction);
            AddAction(CloseActionName, CloseAction);
            AddEditor(DisplayEditorName, DisplayControl);
            AddEditor(OpacityEditorName, OpacityControl);
        }
    }
}
