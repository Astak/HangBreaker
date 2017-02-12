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
        
        private static void StartNewTicket() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.DoAction(TestMainView.StartActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, "T123456");
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
        }

        private static void WaitFor(int interval) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            for (int i = 0; i < interval; i++) documentManagerService.DoAction(TestMainView.TimerActionName);
        }

        private static TestDocumentManagerService StartMainView() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            IDocument document = documentManagerService.CreateDocument(HangBreaker.Utils.Constants.MainViewName, null, null);
            document.Show();
            return documentManagerService;
        }

    }
}
