using HangBreaker.BusinessModel;
using DevExpress.Mvvm.POCO;
using HangBreaker.Services;
using DevExpress.Xpo;
using System;

namespace HangBreaker.ViewModels {
    public class SetStatusViewModel :ChildViewModel {
        public virtual WorkSessionStatus? Status { get; set; }

        protected virtual IXpoService XpoService {
            get { throw new System.NotImplementedException(); }
        }

        public void Ok() {
            UnitOfWork uow = XpoService.GetUnitOfWork();
            var workSession = uow.GetObjectByKey<WorkSession>(ID);
            workSession.Status = Status.Value;
            workSession.EndTime = DateTime.Now;
            uow.CommitChanges();
            Close();
        }

        public bool CanOk() {
            return Status.HasValue;
        }

        protected virtual void OnStatusChanged() {
            this.RaiseCanExecuteChanged(vm => vm.Ok());
        }
    }
}
