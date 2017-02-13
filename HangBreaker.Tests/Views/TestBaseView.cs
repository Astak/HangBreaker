using DevExpress.Utils.MVVM;
using HangBreaker.Tests.Utils;
using System.Collections.Generic;
using System.Globalization;

namespace HangBreaker.Tests.Views {
    public abstract class TestBaseView {
        private readonly Dictionary<string, TestAction> Actions = new Dictionary<string, TestAction>();
        private readonly Dictionary<string, TestControlBase> Editors = new Dictionary<string, TestControlBase>();

        public TestBaseView() {
            fContext = new MVVMContext();
        }

        private MVVMContext fContext;
        protected MVVMContext Context {
            get { return fContext; }
        }

        public abstract object ViewModel { get; }

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
            TestAction action = GetAction(actionName);
            action.Execute();
        }

        public void SetEditorValue(string editorName, object value) {
            TestControlBase editor = GetEditor(editorName);
            editor.ValueCore = value;
        }

        public object GetEditorValue(string editorName) {
            TestControlBase editor = GetEditor(editorName);
            return editor.ValueCore;
        }

        public bool CanDoAction(string actionName) {
            TestAction action = GetAction(actionName);
            return action.Enabled;
        }

        protected void AddAction(string name, TestAction action) {
            Actions.Add(name, action);
        }

        protected void AddEditor(string editorName, TestControlBase editor) {
            Editors.Add(editorName, editor);
        }

        private TestControlBase GetEditor(string editorName) {
            TestControlBase editor = null;
            if (Editors.TryGetValue(editorName, out editor)) return editor;
            var msg = string.Format(CultureInfo.CurrentUICulture,
                "{0} view does not have {1} editor",
                GetType().Name, editorName);
            throw new System.InvalidOperationException(msg);
        }

        private TestAction GetAction(string actionName) {
            TestAction action = null;
            if (Actions.TryGetValue(actionName, out action)) return action;
            else {
                var msg = string.Format(CultureInfo.CurrentUICulture,
                    "{0} view does not have {1} action",
                    GetType().Name, actionName);
                throw new System.InvalidOperationException(msg);
            }
        }
    }

    public class TestBaseView<TViewModel> : TestBaseView {
        public TestBaseView() {
            Context.ViewModelType = typeof(TViewModel);
        }

        public override object ViewModel {
            get { return Context.GetViewModel<TViewModel>(); }
        }
        
    }
}
