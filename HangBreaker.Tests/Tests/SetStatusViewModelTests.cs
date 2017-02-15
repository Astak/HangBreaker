using DevExpress.Mvvm;
using DevExpress.Xpo;
using HangBreaker.BusinessModel;
using HangBreaker.Services;
using HangBreaker.Tests.Services;
using HangBreaker.Tests.Services.Documents;
using HangBreaker.Tests.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests {
    [TestClass]
    public class SetStatusViewModelTests {
        [TestCleanup]
        public void Cleanup() {
            var xpoService = (TestXpoService)ServiceContainer.Default.GetService<IXpoService>();
            xpoService.Cleanup();
        }

        [TestMethod]
        public void CannotSaveIfStatusIsNotSpecifiedTest() {
            var workSession = CreateTestWorkSession();
            var view = new TestSetStatusView();
            view.SetParameter(workSession.Oid);
            Assert.IsFalse(view.OKAction.Enabled);
        }

        [TestMethod]
        public void CanSaveIfStatusIsSpecifiedTest() {
            var workSession = CreateTestWorkSession();
            var view = new TestSetStatusView();
            view.SetParameter(workSession.Oid);
            view.StatusControl.Value = WorkSessionStatus.NeedAnswer;
            Assert.IsTrue(view.OKAction.Enabled);
        }

        [TestMethod]
        public void SetStatusUpdatesExistingRecordInSessionTableTest() {
            var workSession = CreateTestWorkSession();
            TestDocumentManagerService documentManagerService = SharedMethods.StartView(HangBreaker.Utils.Constants.SetStatusViewName, workSession.Oid);
            documentManagerService.SetEditorValue(TestSetStatusView.SetStatusEditorName, WorkSessionStatus.NeedExample);
            documentManagerService.DoAction(TestSetStatusView.OKActionName);
            workSession.Reload();
            Assert.AreEqual<WorkSessionStatus>(WorkSessionStatus.NeedExample, workSession.Status.Value);
        }

        private static WorkSession CreateTestWorkSession() {
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            UnitOfWork uow = xpoService.GetUnitOfWork();
            var workSession = new WorkSession(uow);
            uow.CommitChanges();
            return workSession;
        }
    }
}
