using DevExpress.Utils.MVVM;
using HangBreaker.Tests.Utils;
using System.Collections.Generic;
using System.Globalization;

namespace HangBreaker.Tests.Views {
    public class TestBaseView {
        private readonly Dictionary<string, TestAction> Actions = new Dictionary<string, TestAction>();
        private readonly Dictionary<string, TestControlBase> Editors = new Dictionary<string, TestControlBase>();

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

        public void DoAction(string actionName) {
            TestAction action = null;
            if (Actions.TryGetValue(actionName, out action)) action.Execute();
            else {
                var msg = string.Format(CultureInfo.CurrentUICulture, 
                    "{0} view does not have {1} action", 
                    GetType().Name, actionName);
                throw new System.InvalidOperationException(msg);
            }
        }

        public void SetEditorValue(string editorName, object value) {
            TestControlBase editor = null;
            if (Editors.TryGetValue(editorName, out editor)) editor.SetValue(value);
            else {
                var msg = string.Format(CultureInfo.CurrentUICulture,
                    "{0} view does not have {1} editor",
                    GetType().Name, editorName);
                throw new System.InvalidOperationException(msg);
            }
        }

        protected void AddAction(string name, TestAction action) {
            Actions.Add(name, action);
        }

        protected void AddEditor(string editorName, TestControl<string> editor) {
            Editors.Add(editorName, editor);
        }
    }
}
