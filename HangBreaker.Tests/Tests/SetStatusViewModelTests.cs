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
    }
}
