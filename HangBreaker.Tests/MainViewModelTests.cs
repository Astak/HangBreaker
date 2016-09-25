using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests {
    [TestClass]
    public class MainViewModelTests {
        [TestMethod]
        public void StartActionTest() {
            var mainView = new TestMainView();
            mainView.TestInitialState();
            mainView.StartAction.Execute();
            mainView.TestReviewState();
            var interval = 5 * 60;
            for (int i = 0; i < interval; i++) {
                if (i == 24) {
                    mainView.TestReviewState();
                    Assert.AreEqual<string>("00:14:36", mainView.DisplayControl.Value);
                }
                mainView.TimerAction.Execute();
            }
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            mainView.TestWorkState();
            interval = 10 * 60;
            for (int i = 0; i < interval; i++) {
                if (i == 230) {
                    mainView.TestWorkState();
                    Assert.AreEqual<string>("00:06:10", mainView.DisplayControl.Value);
                }
                if (i == 402) {
                    mainView.TestWorkState();
                    Assert.AreEqual<string>("00:03:18", mainView.DisplayControl.Value);
                }
                mainView.TimerAction.Execute();
            }
            mainView.TestWorkOverflowState();
            mainView.Invalidate();
        }
    }
}
