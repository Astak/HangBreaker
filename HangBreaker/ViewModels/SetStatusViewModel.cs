using HangBreaker.BusinessModel;
using DevExpress.Mvvm.POCO;
using HangBreaker.Services;
using DevExpress.Xpo;
using DevExpress.Mvvm;

namespace HangBreaker.ViewModels {
    public class SetStatusViewModel :ISupportParameter {
        public virtual WorkSessionStatus? Status { get; set; }
        protected virtual SetStatusViewModelParameters Parameter { get; set; }

        protected virtual IXpoService XpoService {
            get { throw new System.NotImplementedException(); }
        }

        public void Ok() {
            UnitOfWork uow = XpoService.GetUnitOfWork();
            var workSession = uow.GetObjectByKey<WorkSession>(Parameter.WorkSessionId);
            if (Parameter.IsFinal)
                workSession.FinalStatus = Status.Value;
            else workSession.IntermediateStatus = Status.Value;
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
            get { return Parameter; }
            set { Parameter = (SetStatusViewModelParameters)value; }
        }
        #endregion
    }

    public class SetStatusViewModelParameters {
        public SetStatusViewModelParameters(int workSessionId, bool isFinal) {
            this.WorkSessionId = workSessionId;
            this.IsFinal = isFinal;
        }
        
        public int WorkSessionId { get; private set; }
        public bool IsFinal { get; private set; }
    }
}
