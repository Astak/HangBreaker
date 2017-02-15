using DevExpress.Mvvm;
using HangBreaker.BusinessModel;
using HangBreaker.Tests.Services.Documents;
using HangBreaker.Tests.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests {
    public static class SharedMethods {
        public static TestDocumentManagerService StartView(string viewName, object parameter = null) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            IDocument document = documentManagerService.CreateDocument(viewName, parameter, null);
            document.Show();
            return documentManagerService;
        }

        public static void StartNewTicket(string ticketId) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.DoAction(TestMainView.StartActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, ticketId);
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
        }

        public static void RestartSession(string ticketId) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.DoAction(TestMainView.RestartActionName);
            documentManagerService.SetEditorValue(TestSetStatusView.SetStatusEditorName, WorkSessionStatus.NeedExample);
            documentManagerService.DoAction(TestSetStatusView.OKActionName);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, ticketId);
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
        }

        public static void WaitFor(int interval) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            for (int i = 0; i < interval; i++) documentManagerService.DoAction(TestMainView.TimerActionName);
        }

        public static void TestInitialState() {
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

        public static void TestReviewState() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            bool canDoStart = documentManagerService.CanDoAction(TestMainView.StartActionName);
            bool canDoRestart = documentManagerService.CanDoAction(TestMainView.RestartActionName);
            var opacityValue = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsFalse(canDoStart);
            Assert.IsTrue(canDoRestart);
            Assert.IsTrue(opacityValue);
        }

        public static void TestReviewOverflowState() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            bool canDoStart = documentManagerService.CanDoAction(TestMainView.StartActionName);
            bool canDoRestart = documentManagerService.CanDoAction(TestMainView.RestartActionName);
            var displayValue = documentManagerService.GetEditorValue<string>(TestMainView.DisplayEditorName);
            Assert.IsTrue(canDoStart);
            Assert.IsTrue(canDoRestart);
            Assert.AreEqual<string>("Overtime", displayValue);
        }

        public static void TestWorkState() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            bool canDoStart = documentManagerService.CanDoAction(TestMainView.StartActionName);
            bool canDoRestart = documentManagerService.CanDoAction(TestMainView.RestartActionName);
            var opacityValue = documentManagerService.GetEditorValue<bool>(TestMainView.OpacityEditorName);
            Assert.IsFalse(canDoStart);
            Assert.IsTrue(canDoRestart);
            Assert.IsTrue(opacityValue);
        }

        public static void TestWorkOverflowState() {
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
