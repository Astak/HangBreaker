using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using DevExpress.Utils.MVVM.Services;
using HangBreaker.Documents;

namespace HangBreaker.Tests.UI {
    [TestClass]
    public class UserControlDocumentAdapterTest {
        [TestMethod]
        public void ShowAddsToForm() {
            var form = new Form();
            var control = new Control();
            IDocumentAdapter adapter = new UserControlDocumentAdapter(form);
            adapter.Show(control);
            Assert.AreEqual(form, control.Parent);
        }

        [TestMethod]
        public void ShowDocksFill() {
            var form = new Form();
            var control = new Control();
            IDocumentAdapter adapter = new UserControlDocumentAdapter(form);
            adapter.Show(control);
            Assert.AreEqual<DockStyle>(DockStyle.Fill, control.Dock);
        }
    }
}
