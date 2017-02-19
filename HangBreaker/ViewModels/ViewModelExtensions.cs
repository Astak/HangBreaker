using DevExpress.Mvvm;
using HangBreaker.Utils;
using System.Threading.Tasks;

namespace HangBreaker.ViewModels {
    public static class ViewModelExtensions {
        public static void CloseDocument(this object viewModel) {
            var documentManagerService = ServiceContainer.Default.GetService<IDocumentManagerService>(Constants.ServiceKey);
            IDocument document = documentManagerService.FindDocument(viewModel);
            document.Close();
        }

        public static Task<TParameter> ShowDocumentAsync<TParameter>(this IDocumentManagerService documentManagerService, string documentType, TParameter parameter, object parentViewModel) {
            IDocument document = documentManagerService.CreateDocument(documentType, parameter, parentViewModel);
            document.Show();
            ActiveDocumentChangedEventHandler handler = null;
            var taskCompletionSource = new TaskCompletionSource<TParameter>();
            handler = (s, e) => {
                documentManagerService.ActiveDocumentChanged -= handler;
                taskCompletionSource.SetResult(parameter);
            };
            documentManagerService.ActiveDocumentChanged += handler;
            return taskCompletionSource.Task;
        }
    }
}
