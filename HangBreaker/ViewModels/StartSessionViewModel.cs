using DevExpress.Mvvm.POCO;
using DevExpress.Xpo;
using HangBreaker.BusinessModel;
using HangBreaker.Services;

namespace HangBreaker.ViewModels {
    public class StartSessionViewModel {
        public virtual string TicketID { get; set; }

        protected virtual IXpoService XpoService {
            get { throw new System.NotImplementedException(); }
        }

        public void Start() {
            using (UnitOfWork uow = XpoService.GetUnitOfWork()) {
                var session = new WorkSession(uow);
                session.TicketID = TicketID;
                uow.CommitChanges();
            }
        }

        public bool CanStart() {
            return !string.IsNullOrEmpty(TicketID);
        }

        protected virtual void OnTicketIDChanged() {
            this.RaiseCanExecuteChanged(vm => vm.Start());
        }
    }
}
