using DevExpress.Mvvm;
using System.Threading.Tasks;

namespace HangBreaker.ViewModels {
    public class ChildViewModel : BaseViewModel, ISupportParameter {
        public ChildViewModel() {
            Promise = new TaskCompletionSource<int>();
        }

        protected int ID { get; private set; }
        public TaskCompletionSource<int> Promise { get; private set; }

        public override void  Close() {
            base.Close();
            Promise.SetResult(ID);
        }
        #region ISupportParameter
        object ISupportParameter.Parameter {
            get { return ID; }
            set { ID = (int)value; }
        }
        #endregion
    }
}
