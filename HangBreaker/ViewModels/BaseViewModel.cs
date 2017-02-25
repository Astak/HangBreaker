using DevExpress.Mvvm;

namespace HangBreaker.ViewModels {
    public class BaseViewModel {
        protected IDocumentManagerService DocumentManagerService {
            get { return ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey); }
        }

        public virtual void Close() {
            DocumentManagerService.CloseDocument(this);
        }
    }
}
