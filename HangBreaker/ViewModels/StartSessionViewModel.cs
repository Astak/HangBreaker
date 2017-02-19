using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpo;
using HangBreaker.BusinessModel;
using HangBreaker.Services;
using HangBreaker.Utils;

namespace HangBreaker.ViewModels {
    public class StartSessionViewModel :ISupportParameter {
        private int WorkSessionKey;

        public virtual string TicketID { get; set; }

        protected virtual IXpoService XpoService {
            get { throw new System.NotImplementedException(); }
        }

        protected IDocumentManagerService DocumentManagerService {
            get { return ServiceContainer.Default.GetService<IDocumentManagerService>(Constants.ServiceKey); }
        }

        public void Start() {
            using (UnitOfWork uow = XpoService.GetUnitOfWork()) {
                var session = uow.GetObjectByKey<WorkSession>(WorkSessionKey);
                session.TicketID = TicketID;
                uow.CommitChanges();
            }
            this.CloseDocument();
        }

        public bool CanStart() {
            return !string.IsNullOrEmpty(TicketID);
        }

        protected virtual void OnTicketIDChanged() {
            this.RaiseCanExecuteChanged(vm => vm.Start());
        }
        #region ISupportParameter
        object ISupportParameter.Parameter {
            get { return WorkSessionKey; }
            set { WorkSessionKey = (int)value; }
        }
        #endregion
    }
}
