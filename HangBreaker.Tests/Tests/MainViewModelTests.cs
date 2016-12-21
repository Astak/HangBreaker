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
            var mainView = new TestMainView();
            mainView.StartAction.Execute();
            WaitFor(147);
            mainView.TestReviewState();
            mainView.RestartAction.Execute();
            int interval = 187;
            WaitFor(interval);
            mainView.TestReviewState();
            WaitFor(ReviewInterval - interval);
            mainView.TestReviewOverflowState();
            mainView.RestartAction.Execute();
            interval = 39;
            WaitFor(interval);
            mainView.TestReviewState();
            WaitFor(ReviewInterval - interval);
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            WaitFor(66);
            mainView.TestWorkState();
            mainView.RestartAction.Execute();
            interval = 154;
            WaitFor(interval);
            mainView.TestReviewState();
            WaitFor(ReviewInterval - interval);
            mainView.TestReviewOverflowState();
            mainView.StartAction.Execute();
            WaitFor(WorkInterval);
            mainView.TestWorkOverflowState();
            mainView.RestartAction.Execute();
            WaitFor(276);
            mainView.TestReviewState();
            mainView.Invalidate();
        }

        [TestMethod]
        public void DisplayTest() {
            //var mainView = new TestMainView();
            //Assert.AreEqual<string>("Hello", mainView.DisplayControl.Value);
            //mainView.StartAction.Execute();
            //Assert.AreEqual<string>("00:15:00", mainView.DisplayControl.Value);
            //int interval = 19;
            //mainView.WaitFor(interval);
            //Assert.AreEqual<string>("00:14:41", mainView.DisplayControl.Value);
            //mainView.WaitFor(ReviewInterval - interval - 1);
            //Assert.AreEqual<string>("00:10:01", mainView.DisplayControl.Value);
            //mainView.WaitFor(1);
            //Assert.AreEqual<string>("Overtime", mainView.DisplayControl.Value);
            //mainView.StartAction.Execute();
            //Assert.AreEqual<string>("00:10:00", mainView.DisplayControl.Value);
            //interval = 2;
            //mainView.WaitFor(interval);
            //Assert.AreEqual<string>("00:09:58", mainView.DisplayControl.Value);
            //mainView.WaitFor(WorkInterval - interval - 1);
            //Assert.AreEqual<string>("00:00:01", mainView.DisplayControl.Value);
            //mainView.WaitFor(1);
            //Assert.AreEqual<string>("Overtime", mainView.DisplayControl.Value);
            //mainView.Invalidate();
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            IDocument document = documentManagerService.CreateDocument(HangBreaker.Utils.Constants.MainViewName, null, null);
            document.Show();
            var displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.AreEqual("Hello", displayValue);
            documentManagerService.DoAction(TestMainView.StartActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, "T123456");
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
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
        }

        private void WaitFor(int interval) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            for (int i = 0; i < interval; i++) documentManagerService.DoAction(TestMainView.TimerActionName);
        }

        [TestMethod]
        public void OpacityTest() {
            var mainView = new TestMainView();
            mainView.StartAction.Execute();
            WaitFor(ReviewInterval);
            Assert.IsTrue(mainView.OpacityControl.Value);
            WaitFor(1);
            Assert.IsFalse(mainView.OpacityControl.Value);
            WaitFor(273);
            Assert.IsTrue(mainView.OpacityControl.Value);
            WaitFor(284);
            Assert.IsTrue(mainView.OpacityControl.Value);
            WaitFor(WorkInterval);
            Assert.IsTrue(mainView.OpacityControl.Value);
            WaitFor(109);
            Assert.IsFalse(mainView.OpacityControl.Value);
            WaitFor(213);
            Assert.IsTrue(mainView.OpacityControl.Value);
            mainView.Invalidate();
        }
        
        [TestMethod]
        public void RefreshSetsStatusAndEndTime() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.CreateDocument(Constants.MainViewName, null, null).Show();
            documentManagerService.DoAction(TestMainView.StartActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, "T123456");
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
            documentManagerService.DoAction(TestMainView.RestartActionName);
            documentManagerService.SetEditorValue(TestSetStatusView.SetStatusEditorName, WorkSessionStatus.NeedAnswer);
            documentManagerService.DoAction(TestSetStatusView.OKActionName);
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            Session session = xpoService.GetSession();
            var workSession = session.Query<WorkSession>()
                .Select(s => new { s.Status, s.EndTime })
                .Single();
            Assert.AreEqual(WorkSessionStatus.NeedAnswer, workSession.Status);
            Assert.IsNotNull(workSession.EndTime);
        }

        [TestMethod]
        public void RestartActionShouldCreateNewSession() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.CreateDocument(Constants.MainViewName, null, null).Show();
            documentManagerService.DoAction(TestMainView.StartActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, "T123456");
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
            documentManagerService.DoAction(TestMainView.RestartActionName);
            documentManagerService.SetEditorValue(TestSetStatusView.SetStatusEditorName, WorkSessionStatus.NeedAnswer);
            documentManagerService.DoAction(TestSetStatusView.OKActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, "123457");
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            using (Session session = xpoService.GetSession()) {
                int sessionCnt = session.Query<WorkSession>().Count();
                Assert.AreEqual(2, sessionCnt);
            }
        }
    }
}
