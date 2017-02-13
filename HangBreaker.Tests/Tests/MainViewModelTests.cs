using DevExpress.Mvvm;
using DevExpress.Xpo;
using HangBreaker.BusinessModel;
using HangBreaker.Services;
using HangBreaker.Tests.Services;
using HangBreaker.Tests.Services.Documents;
using HangBreaker.Tests.Utils;
using HangBreaker.Tests.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HangBreaker.Tests {
    [TestClass]
    public class MainViewModelTests {
        private const int ReviewInterval = 5 * 60;
        private const int WorkInterval = 10 * 60;

        [TestCleanup]
        public void Cleanup() {
            var xpoService = (TestXpoService)ServiceContainer.Default.GetService<IXpoService>();
            xpoService.Cleanup();
        }

        [TestMethod]
        public void StartActionTest() {
            TestDocumentManagerService documentManagerService = StartMainView();
            TestInitialState();
            StartNewTicket("T123456");
            TestReviewState();
            WaitFor(ReviewInterval);
            TestReviewOverflowState();
            documentManagerService.DoAction(TestMainView.StartActionName);
            TestWorkState();
            WaitFor(WorkInterval);
            TestWorkOverflowState();
            documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void RestartActionTest() {
            TestDocumentManagerService documentManagerService = StartMainView();
            StartNewTicket("T123456");
            WaitFor(147);
            TestReviewState();
            RestartSession("T123457");
            int interval = 187;
            WaitFor(interval);
            TestReviewState();
            WaitFor(ReviewInterval - interval);
            TestReviewOverflowState();
            RestartSession("T123458");
            interval = 39;
            WaitFor(interval);
            TestReviewState();
            WaitFor(ReviewInterval - interval);
            TestReviewOverflowState();
            documentManagerService.DoAction(TestMainView.StartActionName);
            WaitFor(66);
            TestWorkState();
            RestartSession("T123459");
            interval = 154;
            WaitFor(interval);
            TestReviewState();
            WaitFor(ReviewInterval - interval);
            TestReviewOverflowState();
            documentManagerService.DoAction(TestMainView.StartActionName);
            WaitFor(WorkInterval);
            TestWorkOverflowState();
            RestartSession("T123460");
            WaitFor(276);
            TestReviewState();
            documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void DisplayTest() {
            TestDocumentManagerService documentManagerService = StartMainView();
            var displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("Hello", displayValue);
            StartNewTicket("T123456");
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
            documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void OpacityTest() {
            TestDocumentManagerService documentManagerService = StartMainView();
            StartNewTicket("T123456");
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
            documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void StartSessionSetsStartTime() {
            TestDocumentManagerService documentManagerService = StartMainView();
            StartNewTicket("T123456");
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            Session session = xpoService.GetSession();
            DateTime from = DateTime.Now.AddSeconds(-1);
            DateTime to = DateTime.Now.AddSeconds(1);
            var workSession = session.Query<WorkSession>()
                .Where(s => s.TicketID == "T123456" &&
                    s.StartTime > from && s.StartTime <= to)
                .Single();
        }

        private static TestDocumentManagerService StartMainView() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            IDocument document = documentManagerService.CreateDocument(HangBreaker.Utils.Constants.MainViewName, null, null);
            document.Show();
            return documentManagerService;
        }

        private static void StartNewTicket(string ticketId) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.DoAction(TestMainView.StartActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, ticketId);
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
        }

        private static void RestartSession(string ticketId) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.DoAction(TestMainView.RestartActionName);
            documentManagerService.SetEditorValue(TestSetStatusView.SetStatusEditorName, WorkSessionStatus.NeedExample);
            documentManagerService.DoAction(TestSetStatusView.OKActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, ticketId);
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
