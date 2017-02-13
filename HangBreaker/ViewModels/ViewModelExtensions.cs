using DevExpress.Mvvm;
using HangBreaker.Utils;

namespace HangBreaker.ViewModels {
    public static class ViewModelExtensions {
        public static void CloseDocument(this object viewModel) {
            var documentManagerService = ServiceContainer.Default.GetService<IDocumentManagerService>(Constants.ServiceKey);
            IDocument document = documentManagerService.FindDocument(viewModel);
            document.Close();
        }
    }
}
