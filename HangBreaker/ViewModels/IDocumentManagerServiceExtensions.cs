using DevExpress.Mvvm;
using System.Threading.Tasks;

namespace HangBreaker.ViewModels {
    public static class IDocumentManagerServiceExtensions {
        public static void CloseDocument(this IDocumentManagerService documentManagerService, object viewModel) {
            IDocument document = documentManagerService.FindDocument(viewModel);
            document.Close();
        }

        public static Task<int> ShowDocumentAsync(this IDocumentManagerService documentManagerService, string documentType, int parameter, object parentViewModel) {
            IDocument document = documentManagerService.CreateDocument(documentType, parameter, parentViewModel);
            document.Show();
            var childViewModel = (ChildViewModel)document.Content;
            return childViewModel.Promise.Task;
        }
    }
}
