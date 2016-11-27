﻿using DevExpress.Utils.MVVM;
using HangBreaker.Tests.Utils;
using HangBreaker.ViewModels;

namespace HangBreaker.Tests.Views {
    public class TestStartSessionView {
        private readonly MVVMContext Context;
        public readonly TestAction StartAction;
        public readonly TestControl<string> TicketIDControl;

        public TestStartSessionView() {
            Context = new MVVMContext();
            Context.ViewModelType = typeof(StartSessionViewModel);
            StartAction = new TestAction();
            TicketIDControl = new TestControl<string>();
            var api = Context.OfType<StartSessionViewModel>();
            api.BindCommand(StartAction, vm => vm.Start());
            api.SetBinding(TicketIDControl, ctrl => ctrl.Value, vm => vm.TicketID);
        }
    }
}