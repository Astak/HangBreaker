using DevExpress.Mvvm;
using HangBreaker.Tests.Utils;
using HangBreaker.Tests.Views;

namespace HangBreaker.Tests.Services.Documents {
    public sealed class TestDocument :IDocument {
        private TestBaseView fContent;

        public TestDocument(TestBaseView content) {
            fContent = content;
        }

        public void DoAction(string actionName) {
            fContent.DoAction(actionName);
        }

        public void SetEditorValue(string editorName, string value) {
            fContent.SetEditorValue(editorName, value);
        }
        #region IDocument
        void IDocument.Close(bool force) {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.CloseDocument(this);
            fContent.Invalidate();
        }

        object IDocument.Content {
            get { return fContent; }
        }

        bool IDocument.DestroyOnClose { get; set; }

        void IDocument.Hide() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.HideDocument(this);
        }

        object IDocument.Id { get; set; }

        void IDocument.Show() {
            var documentManagerService = (TestDocumentManagerService)ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey);
            documentManagerService.ShowDocument(this);
        }

        object IDocument.Title { get; set; }
        #endregion
    }
}
