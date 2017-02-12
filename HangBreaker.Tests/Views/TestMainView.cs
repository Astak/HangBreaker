using DevExpress.Utils.MVVM;
using HangBreaker.Tests.Utils;
using HangBreaker.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests.Views {
    public class TestMainView :TestBaseView {
        public const string StartActionName = "Start";
        public const string RestartActionName = "Restart";
        public const string DisplayEditorName = "Display";
        public const string TimerActionName = "Timer";
        public const string OpacityEditorName = "Opacity";

        public TestAction StartAction { get; private set; }
        public TestAction RestartAction { get; private set; }
        public TestAction TimerAction { get; private set; }
        public TestControl<string> DisplayControl { get; private set; }
        public TestControl<bool> OpacityControl { get; private set; }

        public TestMainView() {
            Context.ViewModelType = typeof(MainViewModel);
            StartAction = new TestAction();
            RestartAction = new TestAction();
            TimerAction = new TestAction();
            DisplayControl = new TestControl<string>();
            OpacityControl = new TestControl<bool>();
            var api = Context.OfType<MainViewModel>();
            api.BindCommand(StartAction, vm => vm.Start());
            api.BindCommand(RestartAction, vm => vm.Restart());
            api.BindCommand(TimerAction, vm => vm.Tick());
            api.SetBinding(DisplayControl, ctrl => ctrl.Value, vm => vm.DisplayText);
            api.SetBinding(OpacityControl, ctrl => ctrl.Value, vm => vm.IsTransparent);
            AddAction(StartActionName, StartAction);
            AddAction(RestartActionName, RestartAction);
            AddAction(TimerActionName, TimerAction);
            AddEditor(DisplayEditorName, DisplayControl);
            AddEditor(OpacityEditorName, OpacityControl);
        }
    }
}
