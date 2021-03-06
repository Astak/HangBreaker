﻿using DevExpress.Utils.MVVM;
using HangBreaker.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests {
    public class TestMainView {
        private MVVMContext Context;

        public TestAction StartAction { get; private set; }
        public TestAction RestartAction { get; private set; }
        public TestAction TimerAction { get; private set; }
        public TestControl<string> DisplayControl { get; private set; }
        public TestControl<bool> OpacityControl { get; private set; }

        public TestMainView() {
            Context = new MVVMContext();
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
        }

        public void TestInitialState() {
            Assert.IsTrue(StartAction.Enabled);
            Assert.IsFalse(RestartAction.Enabled);
            Assert.AreEqual<string>("Hello", DisplayControl.Value);
            Assert.IsFalse(OpacityControl.Value);
        }

        public void TestReviewState() {
            Assert.IsFalse(StartAction.Enabled);
            Assert.IsTrue(RestartAction.Enabled);
            Assert.IsTrue(OpacityControl.Value);
        }

        public void TestReviewOverflowState() {
            Assert.IsTrue(StartAction.Enabled);
            Assert.IsTrue(RestartAction.Enabled);
            Assert.AreEqual<string>("Overtime", DisplayControl.Value);
        }

        public void TestWorkState() {
            Assert.IsFalse(StartAction.Enabled);
            Assert.IsTrue(RestartAction.Enabled);
            Assert.IsTrue(OpacityControl.Value);
        }

        public void TestWorkOverflowState() {
            Assert.IsFalse(StartAction.Enabled);
            Assert.IsTrue(RestartAction.Enabled);
            Assert.AreEqual<string>("Overtime", DisplayControl.Value);
        }

        public void Invalidate() {
            if (Context == null) return;
            Context.Dispose();
            Context = null;
        }

        public void WaitFor(int seconds) {
            for (int i = 0; i < seconds; i++) TimerAction.Execute();
        }
    }
}
