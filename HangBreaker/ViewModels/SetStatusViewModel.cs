using HangBreaker.BusinessModel;
using DevExpress.Mvvm.POCO;
using HangBreaker.Services;
using DevExpress.Xpo;
using DevExpress.Mvvm;

namespace HangBreaker.ViewModels {
    public class SetStatusViewModel :ISupportParameter {
        public virtual WorkSessionStatus? Status { get; set; }
        protected virtual int SessionId { get; set; }

        protected virtual IXpoService XpoService {
            get { throw new System.NotImplementedException(); }
        }

        public void Ok() {
            UnitOfWork uow = XpoService.GetUnitOfWork();
            var workSession = uow.GetObjectByKey<WorkSession>(SessionId);
            workSession.Status = Status.Value;
            uow.CommitChanges();
        }

        public bool CanOk() {
            return Status.HasValue;
        }

        protected virtual void OnStatusChanged() {
            this.RaiseCanExecuteChanged(vm => vm.Ok());
        }
        #region ISupportParameter
        object ISupportParameter.Parameter {
            get { return SessionId; }
            set { SessionId = (int)value; }
        }
        #endregion
    }
}
