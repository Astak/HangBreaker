using DevExpress.Mvvm;
using DevExpress.Xpo;
using HangBreaker.BusinessModel;
using HangBreaker.Services;
using HangBreaker.Tests.Services.Documents;
using HangBreaker.Tests.Utils;
using HangBreaker.Tests.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
            WaitFor(ReviewInterval);
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            mainView.TestWorkState();
            WaitFor(WorkInterval);
            mainView.TestWorkOverflowState();
            mainView.Invalidate();
        }

        [TestMethod]
        public void RestartActionTest() {
            TestDocumentManagerService documentManagerService = StartMainView();
            StartNewTicket();
            WaitFor(147);
            TestReviewState();
            RestartSession();
            int interval = 187;
            WaitFor(interval);
            TestReviewState();
            WaitFor(ReviewInterval - interval);
            TestReviewOverflowState();
            RestartSession();
            interval = 39;
            WaitFor(interval);
            TestReviewState();
            WaitFor(ReviewInterval - interval);
            TestReviewOverflowState();
            documentManagerService.DoAction(TestMainView.StartActionName);
            WaitFor(66);
            TestWorkState();
            RestartSession();
            interval = 154;
            WaitFor(interval);
            TestReviewState();
            WaitFor(ReviewInterval - interval);
            TestReviewOverflowState();
            documentManagerService.DoAction(TestMainView.StartActionName);
            WaitFor(WorkInterval);
            TestWorkOverflowState();
            RestartSession();
            WaitFor(276);
            TestReviewState();
            // TODO
            //documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void DisplayTest() {
            TestDocumentManagerService documentManagerService = StartMainView();
            var displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("Hello", displayValue);
            StartNewTicket();
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:15:00", displayValue);
            int interval = 19;
            WaitFor(interval);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:14:41", displayValue);
            WaitFor(ReviewInterval - interval - 1);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:10:01", displayValue);
            WaitFor(1);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("Overtime", displayValue);
            documentManagerService.DoAction(TestMainView.StartActionName);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:10:00", displayValue);
            interval = 2;
            WaitFor(interval);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:09:58", displayValue);
            WaitFor(WorkInterval - interval - 1);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:00:01", displayValue);
            WaitFor(1);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("Overtime", displayValue);
            // TODO
            //documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void OpacityTest() {
            TestDocumentManagerService documentManagerService = StartMainView();
            StartNewTicket();
            WaitFor(ReviewInterval);
            var isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            WaitFor(1);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsFalse(isTransparent);
            WaitFor(273);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            WaitFor(284);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            WaitFor(WorkInterval);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            WaitFor(109);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsFalse(isTransparent);
            WaitFor(213);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            // TODO
            //documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        private static TestDocumentManagerService StartMainView() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            IDocument document = documentManagerService.CreateDocument(HangBreaker.Utils.Constants.MainViewName, null, null);
            document.Show();
            return documentManagerService;
        }

        private static void StartNewTicket() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.DoAction(TestMainView.StartActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, "T123456");
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
        }

        private static void RestartSession() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.DoAction(TestMainView.RestartActionName);
            documentManagerService.SetEditorValue(TestSetStatusView.SetStatusEditorName, WorkSessionStatus.NeedExample);
            documentManagerService.DoAction(TestSetStatusView.OKActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, "T123456");
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
        }

        private static void WaitFor(int interval) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            for (int i = 0; i < interval; i++) documentManagerService.DoAction(TestMainView.TimerActionName);
        }

        private static void TestInitialState() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            bool canDoStart = documentManagerService.CanDoAction(TestMainView.StartActionName);
            bool canDoRestart = documentManagerService.CanDoAction(TestMainView.RestartActionName);
            var displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            var opacityValue = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(canDoStart);
            Assert.IsFalse(canDoRestart);
            Assert.AreEqual<string>("Hello", displayValue);
            Assert.IsFalse(opacityValue);
        }

        private static void TestReviewState() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            bool canDoStart = documentManagerService.CanDoAction(TestMainView.StartActionName);
            bool canDoRestart = documentManagerService.CanDoAction(TestMainView.RestartActionName);
            var opacityValue = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsFalse(canDoStart);
            Assert.IsTrue(canDoRestart);
            Assert.IsTrue(opacityValue);
        }

        private static void TestReviewOverflowState() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            bool canDoStart = documentManagerService.CanDoAction(TestMainView.StartActionName);
            bool canDoRestart = documentManagerService.CanDoAction(TestMainView.RestartActionName);
            var displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.IsTrue(canDoStart);
            Assert.IsTrue(canDoRestart);
            Assert.AreEqual<string>("Overtime", displayValue);
        }

        private static void TestWorkState() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            bool canDoStart = documentManagerService.CanDoAction(TestMainView.StartActionName);
            bool canDoRestart = documentManagerService.CanDoAction(TestMainView.RestartActionName);
            var opacityValue = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsFalse(canDoStart);
            Assert.IsTrue(canDoRestart);
            Assert.IsTrue(opacityValue);
        }

        private static void TestWorkOverflowState() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            bool canDoStart = documentManagerService.CanDoAction(TestMainView.StartActionName);
            bool canDoRestart = documentManagerService.CanDoAction(TestMainView.RestartActionName);
            var displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.IsFalse(canDoStart);
            Assert.IsTrue(canDoRestart);
            Assert.AreEqual<string>("Overtime", displayValue);
        }
    }
}
