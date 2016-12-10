﻿using DevExpress.Mvvm;
using HangBreaker.Tests.Views;
using System.Collections.Generic;
using System.Linq;

namespace HangBreaker.Tests.Services.Documents {
    public sealed class TestDocumentManagerService :IDocumentManagerService {
        private IDocument fActiveDocument;
        private ActiveDocumentChangedEventHandler fActiveDocumentChanged;
        private IList<IDocument> fDocuments = new List<IDocument>();

        private void RaiseActiveDocumentChanged(IDocument oldDocument) {
            if (fActiveDocumentChanged == null) return;
            var args = new ActiveDocumentChangedEventArgs(oldDocument, fActiveDocument);
            fActiveDocumentChanged(this, args);
        }

        private void SetActiveDocument(IDocument document) {
            if (fActiveDocument == document) return;
            IDocument oldDocument = fActiveDocument;
            fActiveDocument = document;
            RaiseActiveDocumentChanged(oldDocument);
        }

        public void CloseDocument(IDocument document) {
            fDocuments.Remove(document);
            if (fActiveDocument == document)
                SetActiveDocument(fDocuments.FirstOrDefault());
        }

        public void HideDocument(IDocument document) {
            SetActiveDocument(fDocuments.FirstOrDefault(d => d != document));
        }

        public void ShowDocument(IDocument document) {
            SetActiveDocument(fDocuments.FirstOrDefault(d => d == document));
        }
        #region IDocumentManagerService
        IDocument IDocumentManagerService.ActiveDocument {
            get { return fActiveDocument; }
            set { SetActiveDocument(value); }
        }

        event ActiveDocumentChangedEventHandler IDocumentManagerService.ActiveDocumentChanged {
            add { fActiveDocumentChanged += value; }
            remove { fActiveDocumentChanged -= value; }
        }

        IDocument IDocumentManagerService.CreateDocument(string documentType, object viewModel, object parameter, object parentViewModel) {
            var viewService = ServiceContainer.Default.GetService<IViewService>();
            TestBaseView view = viewService.QueryView(documentType);
            view.SetParameter(parameter);
            view.SetParentViewModel(parentViewModel);
            var result = new TestDocument(view);
            fDocuments.Add(result);
            return result;
        }

        IEnumerable<IDocument> IDocumentManagerService.Documents {
            get { return fDocuments; }
        }
        #endregion
    }
}