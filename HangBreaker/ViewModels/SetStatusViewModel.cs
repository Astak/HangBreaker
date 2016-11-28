using HangBreaker.BusinessModel;
using DevExpress.Mvvm.POCO;

namespace HangBreaker.ViewModels {
    public class SetStatusViewModel {
        public virtual WorkSessionStatus? Status { get; set; }

        public void Ok() {
            throw new System.NotImplementedException();
        }

        public bool CanOk() {
            return Status.HasValue;
        }

        protected virtual void OnStatusChanged() {
            this.RaiseCanExecuteChanged(vm => vm.Ok());
        }
    }
}
