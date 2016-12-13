using DevExpress.Utils.MVVM;
using HangBreaker.Tests.Utils;
using System.Collections.Generic;

namespace HangBreaker.Tests.Views {
    public class TestBaseView {
        private readonly Dictionary<string, TestAction> Actions = new Dictionary<string, TestAction>();
        
        public void DoAction(string actionName) {
            TestAction action = null;
            if (Actions.TryGetValue(actionName, out action)) action.Execute();
            else {
                var msg = string.Format("{0} view does not have {1} action", GetType().Name, actionName);
                throw new System.InvalidOperationException(msg);
            }
        }

        protected void AddAction(string name, TestAction action) {
            Actions.Add(name, action);
        }

        private MVVMContext fContext = new MVVMContext();
        protected MVVMContext Context {
            get { return fContext; }
        }

        public void SetParameter(object parameter) {
            MVVMContext.SetParameter(Context, parameter);
        }

        public void SetParentViewModel(object parentViewModel) {
            MVVMContext.SetParentViewModel(Context, parentViewModel);
        }

        public void Invalidate() {
            if (Context == null) return;
            Context.Dispose();
            fContext = null;
        }
    }
}
