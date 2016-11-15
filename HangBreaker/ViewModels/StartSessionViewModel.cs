using DevExpress.Mvvm.POCO;

namespace HangBreaker.ViewModels {
    public class StartSessionViewModel {
        public virtual string TicketID { get; set; }

        public void Start() {
            throw new System.NotImplementedException();
        }

        public bool CanStart() {
            return !string.IsNullOrEmpty(TicketID);
        }

        protected virtual void OnTicketIDChanged() {
            this.RaiseCanExecuteChanged(vm => vm.Start());
        }
    }
}
