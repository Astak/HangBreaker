using DevExpress.Utils.MVVM;
using System;
using System.Linq.Expressions;

namespace HangBreaker.Tests.Utils {
    public sealed class TestAction :ISupportCommandBinding {
        private Action ExecuteInternal;

        public bool Enabled { get; private set; }

        public void Execute() {
            if (ExecuteInternal != null) ExecuteInternal();
        }
        #region ISupportCommandBinding members
        IDisposable ISupportCommandBinding.BindCommand(Expression<Action> commandSelector, object source, System.Func<object> queryCommandParameter) {
            return CommandHelper.Bind(this, (that, action) => that.ExecuteInternal = action, (that, getState) => that.Enabled = getState(), commandSelector, source, queryCommandParameter);
        }

        IDisposable ISupportCommandBinding.BindCommand<T>(Expression<Action<T>> commandSelector, object source, System.Func<T> queryCommandParameter) {
            return CommandHelper.Bind(this, (that, action) => that.ExecuteInternal = action, (that, getState) => that.Enabled = getState(), commandSelector, source, () => queryCommandParameter == null ? default(T) : queryCommandParameter());
        }

        IDisposable ISupportCommandBinding.BindCommand(object command, Func<object> queryCommandParameter) {
            return CommandHelper.Bind(this, (that, action) => that.ExecuteInternal = action, (that, getState) => that.Enabled = getState(), command, queryCommandParameter);
        }
        #endregion
    }
}
