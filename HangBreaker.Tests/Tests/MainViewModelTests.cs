using HangBreaker.Tests.Views;
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

        [TestMethod]
        public void DisplayTest() {
            var mainView = new TestMainView();
            Assert.AreEqual<string>("Hello", mainView.DisplayControl.Value);
            mainView.StartAction.Execute();
            Assert.AreEqual<string>("00:15:00", mainView.DisplayControl.Value);
            int interval = 19;
            mainView.WaitFor(interval);
            Assert.AreEqual<string>("00:14:41", mainView.DisplayControl.Value);
            mainView.WaitFor(ReviewInterval - interval - 1);
            Assert.AreEqual<string>("00:10:01", mainView.DisplayControl.Value);
            mainView.WaitFor(1);
            Assert.AreEqual<string>("Overtime", mainView.DisplayControl.Value);
            mainView.StartAction.Execute();
            Assert.AreEqual<string>("00:10:00", mainView.DisplayControl.Value);
            interval = 2;
            mainView.WaitFor(interval);
            Assert.AreEqual<string>("00:09:58", mainView.DisplayControl.Value);
            mainView.WaitFor(WorkInterval - interval - 1);
            Assert.AreEqual<string>("00:00:01", mainView.DisplayControl.Value);
            mainView.WaitFor(1);
            Assert.AreEqual<string>("Overtime", mainView.DisplayControl.Value);
            mainView.Invalidate();
        }

        [TestMethod]
        public void OpacityTest() {
            var mainView = new TestMainView();
            mainView.StartAction.Execute();
            mainView.WaitFor(ReviewInterval);
            Assert.IsTrue(mainView.OpacityControl.Value);
            mainView.WaitFor(1);
            Assert.IsFalse(mainView.OpacityControl.Value);
            mainView.WaitFor(273);
            Assert.IsTrue(mainView.OpacityControl.Value);
            mainView.WaitFor(284);
            Assert.IsTrue(mainView.OpacityControl.Value);
            mainView.WaitFor(WorkInterval);
            Assert.IsTrue(mainView.OpacityControl.Value);
            mainView.WaitFor(109);
            Assert.IsFalse(mainView.OpacityControl.Value);
            mainView.WaitFor(213);
            Assert.IsTrue(mainView.OpacityControl.Value);
            mainView.Invalidate();
        }
    }
}
