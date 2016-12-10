using DevExpress.Utils.MVVM;

namespace HangBreaker.Tests.Views {
    public class TestBaseView {
        private MVVMContext fContext = new MVVMContext();
        protected MVVMContext Context {
            get { return fContext; }
        }

        public void SetParameter(object parameter) {
            MVVMContext.SetParameter(Context, parameter);
        }

        public void SetParentViewModel(object parentViewModel) {
            MVVMContext.SetParentViewModel(Context, parentViewModel);
        }

        public void Invalidate() {
            if (Context == null) return;
            Context.Dispose();
            fContext = null;
        }
    }
}
