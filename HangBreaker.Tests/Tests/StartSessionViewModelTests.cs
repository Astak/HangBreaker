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
    public class StartSessionViewModelTests {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context) {
            ServiceContainer.Default.RegisterService(new TestXpoService());
            ServiceContainer.Default.RegisterService(HangBreaker.Utils.Constants.ServiceKey, new TestDocumentManagerService());
            IViewService viewService = new TestViewService();
            viewService.AddResolver(vt => {
                switch (vt) {
                    case Constants.MainViewName: return new TestMainView();
                    case Constants.SetStatusViewName: return new TestSetStatusView();
                    case Constants.StartSessionViewName: return new TestStartSessionView();
                    default: return null;
                }
            });
            ServiceContainer.Default.RegisterService(viewService);
        }

        [TestCleanup]
        public void Cleanup() {
            var xpoService = (TestXpoService)ServiceContainer.Default.GetService<IXpoService>();
            xpoService.Cleanup();
        }

        [TestMethod]
        public void CannotSaveIfTicketIDIsNotSpecified() {
            var view = new TestStartSessionView();
            Assert.IsFalse(view.StartAction.Enabled);
        }

        [TestMethod]
        public void CanSaveIfTicketIDIsSpecified() {
            var view = new TestStartSessionView();
            view.TicketIDControl.Value = "T123456";
            Assert.IsTrue(view.StartAction.Enabled);
        }

        [TestMethod]
        public void StartSessionUpdatesTicketID() {
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            UnitOfWork session = xpoService.GetUnitOfWork();
            var workSession = new WorkSession(session);
            session.CommitChanges();
            TestDocumentManagerService documentManagerService = SharedMethods.StartView(HangBreaker.Utils.Constants.StartSessionViewName, workSession.Oid);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, "T123456");
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
            workSession.Reload();
            Assert.AreEqual("T123456", workSession.TicketID);
        }

        [TestMethod]
        public void StartSessionInitializesStartTime() {
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            UnitOfWork uow = xpoService.GetUnitOfWork();
            var workSession = new WorkSession(uow);
            uow.CommitChanges();
            TestDocumentManagerService documentManagerService = SharedMethods.StartView(HangBreaker.Utils.Constants.StartSessionViewName, workSession.Oid);
            documentManagerService.SetEditorValue(TestStartSessionView.TicketIDEditorName, "T123456");
            documentManagerService.DoAction(TestStartSessionView.OKActionName);
            workSession.Reload();
            DateTime from = DateTime.Now.AddSeconds(-1);
            DateTime to = DateTime.Now.AddSeconds(1);
            Assert.IsTrue(workSession.StartTime > from && workSession.StartTime <= to);
        }
    }
}
