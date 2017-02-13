using HangBreaker.BusinessModel;
using DevExpress.Mvvm.POCO;
using HangBreaker.Services;
using DevExpress.Xpo;
using DevExpress.Mvvm;
using System;

namespace HangBreaker.ViewModels {
    public class SetStatusViewModel :ISupportParameter {
        public virtual WorkSessionStatus? Status { get; set; }
        protected virtual int SessionId { get; set; }

        protected virtual IXpoService XpoService {
            get { throw new System.NotImplementedException(); }
        }

        private IDocumentManagerService DocumentManagerService {
            get { return ServiceContainer.Default.GetService<IDocumentManagerService>(HangBreaker.Utils.Constants.ServiceKey); }
        }

        public void Ok() {
            UnitOfWork uow = XpoService.GetUnitOfWork();
            var workSession = uow.GetObjectByKey<WorkSession>(SessionId);
            workSession.Status = Status.Value;
            workSession.EndTime = DateTime.Now;
            uow.CommitChanges();
            this.CloseDocument();
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
