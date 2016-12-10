﻿using DevExpress.Mvvm;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using HangBreaker.BusinessModel;
using HangBreaker.Services;
using HangBreaker.Tests.Services;
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
        public void StartSessionAddsNewRectordToSessionTable() {
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            Session session = xpoService.GetSession();
            var view = new TestStartSessionView();
            view.TicketIDControl.Value = "T123456";
            view.StartAction.Execute();
            var sessions = session.Query<WorkSession>().Select(ws => ws.TicketID).ToArray();
            Assert.AreEqual(1, sessions.Length);
            Assert.AreEqual("T123456", sessions[0]);
        }

        [TestMethod]
        public void StartSessionInitializesStartTime() {
            var view = new TestStartSessionView();
            view.TicketIDControl.Value = "T123456";
            view.StartAction.Execute();
            var xpoService = ServiceContainer.Default.GetService<IXpoService>();
            Session session = xpoService.GetSession();
            int testResult = session.Query<WorkSession>()
                .Where(s => s.TicketID == "T123456" &&
                    s.StartTime > DateTime.Now.AddSeconds(-1) &&
                    s.StartTime <= DateTime.Now.AddSeconds(1))
               .Count();
            Assert.AreEqual(1, testResult);
        }
    }
}
