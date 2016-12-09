using DevExpress.Mvvm;
using DevExpress.Xpo;
using HangBreaker.BusinessModel;
using HangBreaker.Services;
using HangBreaker.Tests.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangBreaker.Tests {
    [TestClass]
    public class SetStatusViewModelTests {
        [TestMethod]
        public void CannotSaveIfStatusIsNotSpecifiedTest() {
            var workSession = CreateTestWorkSession();
            var view = new TestSetStatusView(workSession.Oid, false);
            Assert.IsFalse(view.OKAction.Enabled);
        }

        [TestMethod]
        public void CanSaveIfStatusIsSpecifiedTest() {
            var workSession = CreateTestWorkSession();
            var view = new TestSetStatusView(workSession.Oid, false);
            view.StatusControl.Value = WorkSessionStatus.NeedAnswer;
            Assert.IsTrue(view.OKAction.Enabled);
        }

        [TestMethod]
        public void SetStatusUpdatesExistingRecordInSessionTableTest() {
            var workSession = CreateTestWorkSession();
            var view = new TestSetStatusView(workSession.Oid, false);
            view.StatusControl.Value = WorkSessionStatus.NeedAnswer;
            view.OKAction.Execute();
            workSession.Reload();
            Assert.AreEqual<WorkSessionStatus>(WorkSessionStatus.NeedAnswer, workSession.IntermediateStatus);
        }

        [TestMethod]
        public void SetFinalStatusUpdatesExistingRecordInSessionTableTest() {
            var workSession = CreateTestWorkSession();
            var view = new TestSetStatusView(workSession.Oid, true);
            view.StatusControl.Value = WorkSessionStatus.NeedAnswer;
            view.OKAction.Execute();
            workSession.Reload();
            Assert.AreEqual<WorkSessionStatus>(WorkSessionStatus.NeedAnswer, workSession.FinalStatus);
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
