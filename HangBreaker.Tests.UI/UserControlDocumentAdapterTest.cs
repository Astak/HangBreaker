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

        [TestMethod]
        public void ShowBringsToFront() {
            var form = new Form();
            var control = new Control();
            var testControl = new Control();
            IDocumentAdapter adapter = new UserControlDocumentAdapter(form);
            adapter.Show(control);
            adapter.Show(testControl);
            int index = form.Controls.GetChildIndex(testControl);
            Assert.AreEqual<int>(0, index);
        }

        [TestMethod]
        public void CloseRemovesFromParent() {
            var form = new Form();
            var control = new Control();
            control.Parent = form;
            IDocumentAdapter adapter = new UserControlDocumentAdapter(form);
            adapter.Close(control);
            Assert.IsNull(control.Parent);
        }

        [TestMethod]
        public void CloseDisposesControl() {
            var form = new Form();
            var control = new Control();
            IDocumentAdapter adapter = new UserControlDocumentAdapter(form);
            adapter.Close(control);
            Assert.IsTrue(control.IsDisposed);
        }

        [TestMethod]
        public void LastClosedDocumentDisposesParent() {
            var form = new Form();
            var control = new Control();
            IDocumentAdapter adapter = new UserControlDocumentAdapter(form);
            adapter.Close(control);
            Assert.IsTrue(form.IsDisposed);
        }
    }
}
