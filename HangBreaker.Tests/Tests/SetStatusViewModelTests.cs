using HangBreaker.BusinessModel;
using HangBreaker.Tests.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests {
    [TestClass]
    public class SetStatusViewModelTests {
        [TestMethod]
        public void CannotSaveIfStatusIsNotSpecifiedTest() {
            var view = new TestSetStatusView();
            Assert.IsFalse(view.OKAction.Enabled);
        }

        [TestMethod]
        public void CanSaveIfStatusIsSpecifiedTest() {
            var view = new TestSetStatusView();
            view.StatusControl.Value = WorkSessionStatus.NeedAnswer;
            Assert.IsTrue(view.OKAction.Enabled);
        }
    }
}
