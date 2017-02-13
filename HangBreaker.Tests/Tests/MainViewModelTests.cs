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
            TestDocumentManagerService documentManagerService = SharedMethods.StartMainView();
            SharedMethods.TestInitialState();
            SharedMethods.StartNewTicket("T123456");
            SharedMethods.TestReviewState();
            SharedMethods.WaitFor(ReviewInterval);
            SharedMethods.TestReviewOverflowState();
            documentManagerService.DoAction(TestMainView.StartActionName);
            SharedMethods.TestWorkState();
            SharedMethods.WaitFor(WorkInterval);
            SharedMethods.TestWorkOverflowState();
            documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void RestartActionTest() {
            TestDocumentManagerService documentManagerService = SharedMethods.StartMainView();
            SharedMethods.StartNewTicket("T123456");
            SharedMethods.WaitFor(147);
            SharedMethods.TestReviewState();
            SharedMethods.RestartSession("T123457");
            int interval = 187;
            SharedMethods.WaitFor(interval);
            SharedMethods.TestReviewState();
            SharedMethods.WaitFor(ReviewInterval - interval);
            SharedMethods.TestReviewOverflowState();
            SharedMethods.RestartSession("T123458");
            interval = 39;
            SharedMethods.WaitFor(interval);
            SharedMethods.TestReviewState();
            SharedMethods.WaitFor(ReviewInterval - interval);
            SharedMethods.TestReviewOverflowState();
            documentManagerService.DoAction(TestMainView.StartActionName);
            SharedMethods.WaitFor(66);
            SharedMethods.TestWorkState();
            SharedMethods.RestartSession("T123459");
            interval = 154;
            SharedMethods.WaitFor(interval);
            SharedMethods.TestReviewState();
            SharedMethods.WaitFor(ReviewInterval - interval);
            SharedMethods.TestReviewOverflowState();
            documentManagerService.DoAction(TestMainView.StartActionName);
            SharedMethods.WaitFor(WorkInterval);
            SharedMethods.TestWorkOverflowState();
            SharedMethods.RestartSession("T123460");
            SharedMethods.WaitFor(276);
            SharedMethods.TestReviewState();
            documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void DisplayTest() {
            TestDocumentManagerService documentManagerService = SharedMethods.StartMainView();
            var displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("Hello", displayValue);
            SharedMethods.StartNewTicket("T123456");
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:15:00", displayValue);
            int interval = 19;
            SharedMethods.WaitFor(interval);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:14:41", displayValue);
            SharedMethods.WaitFor(ReviewInterval - interval - 1);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:10:01", displayValue);
            SharedMethods.WaitFor(1);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("Overtime", displayValue);
            documentManagerService.DoAction(TestMainView.StartActionName);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:10:00", displayValue);
            interval = 2;
            SharedMethods.WaitFor(interval);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:09:58", displayValue);
            SharedMethods.WaitFor(WorkInterval - interval - 1);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("00:00:01", displayValue);
            SharedMethods.WaitFor(1);
            displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("Overtime", displayValue);
            documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void OpacityTest() {
            TestDocumentManagerService documentManagerService = SharedMethods.StartMainView();
            SharedMethods.StartNewTicket("T123456");
            SharedMethods.WaitFor(ReviewInterval);
            var isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            SharedMethods.WaitFor(1);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsFalse(isTransparent);
            SharedMethods.WaitFor(273);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            SharedMethods.WaitFor(284);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            SharedMethods.WaitFor(WorkInterval);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            SharedMethods.WaitFor(109);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsFalse(isTransparent);
            SharedMethods.WaitFor(213);
            isTransparent = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsTrue(isTransparent);
            documentManagerService.DoAction(TestMainView.CloseActionName);
        }

        [TestMethod]
        public void StartSessionSetsStartTime() {
            TestDocumentManagerService documentManagerService = SharedMethods.StartMainView();
            SharedMethods.StartNewTicket("T123456");
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            Session session = xpoService.GetSession();
            DateTime from = DateTime.Now.AddSeconds(-1);
            DateTime to = DateTime.Now.AddSeconds(1);
            session.Query<WorkSession>()
                .Where(s => s.TicketID == "T123456" &&
                    s.StartTime > from && s.StartTime <= to)
                .Single();
        }

        [TestMethod]
        public void RestartSessionSetsEndTime() {
            TestDocumentManagerService documentManagerService = SharedMethods.StartMainView();
            SharedMethods.StartNewTicket("T123456");
            SharedMethods.RestartSession("T123457");
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            Session session = xpoService.GetSession();
            DateTime from = DateTime.Now.AddSeconds(-1);
            DateTime to = DateTime.Now.AddSeconds(1);
            session.Query<WorkSession>()
                .Where(s => s.TicketID == "T123456" &&
                    s.EndTime > from && s.EndTime <= to)
                    .Single();
        }
    }
}
