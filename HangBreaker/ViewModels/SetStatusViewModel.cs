using HangBreaker.BusinessModel;
using DevExpress.Mvvm.POCO;
using HangBreaker.Services;
using DevExpress.Xpo;

namespace HangBreaker.ViewModels {
    public class SetStatusViewModel {
        private readonly int WorkSessionId;
        private readonly bool IsFinal;

        public SetStatusViewModel(SetStatusViewModelParameters parameters) {
            this.WorkSessionId = parameters.WorkSessionId;
            this.IsFinal = parameters.IsFinal;
        }

        public virtual WorkSessionStatus? Status { get; set; }

        protected virtual IXpoService XpoService {
            get { throw new System.NotImplementedException(); }
        }

        public void Ok() {
            UnitOfWork uow = XpoService.GetUnitOfWork();
            var workSession = uow.GetObjectByKey<WorkSession>(WorkSessionId);
            if (IsFinal)
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
