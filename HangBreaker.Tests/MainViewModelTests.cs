using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.MVVM;
using HangBreaker.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests {
    [TestClass]
    public class MainViewModelTests {
        [TestMethod]
        public void StartActionTest() {
            var context = new MVVMContext();
            context.ViewModelType = typeof(MainViewModel);
            var api = context.OfType<MainViewModel>();
            var startAction = new TestAction();
            var restartAction = new TestAction();
            var timerAction = new TestAction();
            var displayControl = new TestControl<string>();
            api.BindCommand(startAction, vm => vm.Start());
            api.BindCommand(restartAction, vm => vm.Restart());
            api.BindCommand(timerAction, vm => vm.Tick());
            api.SetBinding(displayControl, ctrl => ctrl.Value, vm => vm.DisplayText);

            // initial
            Assert.IsTrue(startAction.Enabled);
            Assert.IsFalse(restartAction.Enabled);
            Assert.IsFalse(timerAction.Enabled);
            Assert.AreEqual<string>("Hello", displayControl.Value);
            
            startAction.Execute();
            
            // review
            Assert.IsFalse(startAction.Enabled);
            Assert.IsTrue(restartAction.Enabled);
            Assert.IsTrue(timerAction.Enabled);
            Assert.AreEqual<string>("00:15:00", displayControl.Value);

            var interval = 5 * 60;
            for (int i = 0; i < interval; i++) {
                if (i == 24) {
                    Assert.IsFalse(startAction.Enabled);
                    Assert.IsTrue(restartAction.Enabled);
                    Assert.IsTrue(timerAction.Enabled);
                    Assert.AreEqual<string>("00:14:36", displayControl.Value);
                }
                timerAction.Execute();
            }

            // review overflow
            Assert.IsTrue(startAction.Enabled);
            Assert.IsTrue(restartAction.Enabled);
            Assert.IsFalse(timerAction.Enabled);
            Assert.AreEqual<string>("Overtime", displayControl.Value);
            
            startAction.Execute();
            
            // work
            Assert.IsFalse(startAction.Enabled);
            Assert.IsTrue(restartAction.Enabled);
            Assert.IsTrue(timerAction.Enabled);
            Assert.AreEqual("00:10:00", displayControl.Value);

            interval = 10 * 60;
            for (int i = 0; i < interval; i++) {
                if (i == 230) {
                    Assert.IsFalse(startAction.Enabled);
                    Assert.IsTrue(restartAction.Enabled);
                    Assert.IsTrue(timerAction.Enabled);
                    Assert.AreEqual<string>("00:08:10", displayControl.Value);
                }
                if (i == 402) Assert.AreEqual<string>("00:04:18", displayControl.Value);
                timerAction.Execute();
            }

            // work overflow
            Assert.IsFalse(startAction.Enabled);
            Assert.IsTrue(restartAction.Enabled);
            Assert.IsFalse(timerAction.Enabled);
            Assert.AreEqual<string>("Overtime", displayControl.Value);

            context.Dispose();
        }
    }
}
