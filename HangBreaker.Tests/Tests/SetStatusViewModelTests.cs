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
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            UnitOfWork uow = xpoService.GetUnitOfWork();
            var workSession = new WorkSession(uow);
            uow.CommitChanges();
            var view = new TestSetStatusView(workSession.Oid, false);
            Assert.IsFalse(view.OKAction.Enabled);
        }

        [TestMethod]
        public void CanSaveIfStatusIsSpecifiedTest() {
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            UnitOfWork uow = xpoService.GetUnitOfWork();
            var workSession = new WorkSession(uow);
            uow.CommitChanges();
            var view = new TestSetStatusView(workSession.Oid, false);
            view.StatusControl.Value = WorkSessionStatus.NeedAnswer;
            Assert.IsTrue(view.OKAction.Enabled);
        }

        [TestMethod]
        public void SetStatusUpdatesExistingRecordInSessionTableTest() {
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            UnitOfWork uow = xpoService.GetUnitOfWork();
            var workSession = new WorkSession(uow);
            uow.CommitChanges();
            var view = new TestSetStatusView(workSession.Oid, false);
            view.StatusControl.Value = WorkSessionStatus.NeedAnswer;
            view.OKAction.Execute();
            uow.Reload(workSession);
            Assert.AreEqual<WorkSessionStatus>(WorkSessionStatus.NeedAnswer, workSession.IntermediateStatus);
        }
    }
}
