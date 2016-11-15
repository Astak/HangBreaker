using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using HangBreaker.BusinessModel;
using HangBreaker.Tests.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HangBreaker.Tests {
    [TestClass]
    public class StartSessionViewModelTests {
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
        public void StartSessionAddsNewRectordToSessionTable() {
            var prov = new InMemoryDataStore();
            var dal = new SimpleDataLayer(prov);
            var session = new Session(dal);
            var view = new TestStartSessionView();
            view.TicketIDControl.Value = "T123456";
            view.StartAction.Execute();
            var sessions = session.Query<WorkSession>().Select(ws => ws.TicketID).ToArray();
            Assert.AreEqual(1, sessions.Length);
            Assert.AreEqual("T123456", sessions[0]);
        }
    }
}
