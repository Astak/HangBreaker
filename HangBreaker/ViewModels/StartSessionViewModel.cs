using DevExpress.Mvvm.POCO;
using DevExpress.Xpo;
using HangBreaker.BusinessModel;
using HangBreaker.Services;

namespace HangBreaker.ViewModels {
    public class StartSessionViewModel :ChildViewModel {
        public virtual string TicketID { get; set; }

        protected virtual IXpoService XpoService {
            get { throw new System.NotImplementedException(); }
        }

        public void Start() {
            using (UnitOfWork uow = XpoService.GetUnitOfWork()) {
                var session = uow.GetObjectByKey<WorkSession>(ID);
                session.TicketID = TicketID;
                uow.CommitChanges();
            }
            Close();
        }

        public bool CanStart() {
            return !string.IsNullOrEmpty(TicketID);
        }

        protected virtual void OnTicketIDChanged() {
            this.RaiseCanExecuteChanged(vm => vm.Start());
        }
    }
}
