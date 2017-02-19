using DevExpress.Utils.MVVM.Services;
using HangBreaker.Documents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HangBreaker.Tests.UI {
    [TestClass]
    public class UserControlDocumentAdapterFactoryTest {
        [TestMethod]
        public void CreateReturnsUserControlDocumentAdapter() {
            IDocumentAdapterFactory factory = new MainForm();
            IDocumentAdapter adapter = factory.Create();
            Assert.AreEqual<Type>(typeof(UserControlDocumentAdapter), adapter.GetType());
        }
    }
}
