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
            int interval = 5 * 60;
            for (int i = 0; i < interval; i++) mainView.TimerAction.Execute();
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            mainView.TestWorkState();
            interval = 10 * 60;
            for (int i = 0; i < interval; i++) mainView.TimerAction.Execute();
            mainView.TestWorkOverflowState();
            mainView.Invalidate();
        }

        [TestMethod]
        public void RestartActionTest() {
            var mainView = new TestMainView();
            mainView.StartAction.Execute();
            int interval = 147;
            for (int i = 0; i < interval; i++) mainView.TimerAction.Execute();
            mainView.TestReviewState();
            mainView.RestartAction.Execute();
            int testInterval1 = 60 * 3 + 7;
            for (int i = 0; i < testInterval1; i++) mainView.TimerAction.Execute();
            mainView.TestReviewState();
            interval = 5 * 60 - testInterval1;
            for (int i = 0; i < interval; i++) mainView.TimerAction.Execute();
            mainView.TestReviewOverflowState();
            mainView.RestartAction.Execute();
            int testInterval2 = 39;
            for (int i = 0; i < testInterval2; i++) mainView.TimerAction.Execute();
            mainView.TestReviewState();
            interval = 60 * 5 - testInterval2;
            for (int i = 0; i < interval; i++) mainView.TimerAction.Execute();
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            interval = 66;
            for (int i = 0; i < interval; i++) mainView.TimerAction.Execute();
            mainView.TestWorkState();
            mainView.RestartAction.Execute();
            int testInterval3 = 2 * 60 + 24;
            for (int i = 0; i < testInterval3; i++) mainView.TimerAction.Execute();
            mainView.TestReviewState();
            interval = 5 * 60 - testInterval3;
            for (int i = 0; i < interval; i++) mainView.TimerAction.Execute();
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            interval = 10 * 60;
            for (int i = 0; i < interval; i++) mainView.TimerAction.Execute();
            mainView.TestWorkOverflowState();
            mainView.RestartAction.Execute();
            int testInterval4 = 4 * 60 + 36;
            for (int i = 0; i < testInterval4; i++) mainView.TimerAction.Execute();
            mainView.TestReviewState();
            mainView.Invalidate();
        }
    }
}
