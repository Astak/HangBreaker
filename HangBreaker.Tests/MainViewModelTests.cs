using DevExpress.Mvvm.POCO;
using HangBreaker.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests {
    [TestClass]
    public class MainViewModelTests {
        [TestMethod]
        public void StartActionTest() {
            var viewModel = ViewModelSource.Create<MainViewModel>();
            Assert.IsTrue(viewModel.CanStart());
            Assert.IsFalse(viewModel.CanRestart());
            viewModel.Start();
            Assert.IsFalse(viewModel.CanStart());
            Assert.IsTrue(viewModel.CanRestart());
            var interval = 5 * 60;
            for (int i = 0; i < interval; i++) viewModel.Tick();
            Assert.IsTrue(viewModel.CanStart());
            Assert.IsTrue(viewModel.CanRestart());
            viewModel.Start();
            Assert.IsFalse(viewModel.CanStart());
            Assert.IsTrue(viewModel.CanRestart());
            interval = 10 * 60;
            for (int i = 0; i < interval; i++) viewModel.Tick();
            Assert.IsFalse(viewModel.CanStart());
            Assert.IsTrue(viewModel.CanRestart());
        }
    }
}
