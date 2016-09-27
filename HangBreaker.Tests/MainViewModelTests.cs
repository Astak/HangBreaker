using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests {
    [TestClass]
    public class MainViewModelTests {
        private const int ReviewInterval = 5 * 60;
        private const int WorkInterval = 10 * 60;

        [TestMethod]
        public void StartActionTest() {
            var mainView = new TestMainView();
            mainView.TestInitialState();
            mainView.StartAction.Execute();
            mainView.TestReviewState();
            mainView.WaitFor(ReviewInterval);
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            mainView.TestWorkState();
            mainView.WaitFor(WorkInterval);
            mainView.TestWorkOverflowState();
            mainView.Invalidate();
        }

        [TestMethod]
        public void RestartActionTest() {
            var mainView = new TestMainView();
            mainView.StartAction.Execute();
            mainView.WaitFor(147);
            mainView.TestReviewState();
            mainView.RestartAction.Execute();
            int interval = 187;
            mainView.WaitFor(interval);
            mainView.TestReviewState();
            mainView.WaitFor(ReviewInterval - interval);
            mainView.TestReviewOverflowState();
            mainView.RestartAction.Execute();
            interval = 39;
            mainView.WaitFor(interval);
            mainView.TestReviewState();
            mainView.WaitFor(ReviewInterval - interval);
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            mainView.WaitFor(66);
            mainView.TestWorkState();
            mainView.RestartAction.Execute();
            interval = 154;
            mainView.WaitFor(interval);
            mainView.TestReviewState();
            mainView.WaitFor(ReviewInterval - interval);
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            mainView.WaitFor(WorkInterval);
            mainView.TestWorkOverflowState();
            mainView.RestartAction.Execute();
            mainView.WaitFor(276);
            mainView.TestReviewState();
            mainView.Invalidate();
        }
    }
}
