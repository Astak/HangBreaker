﻿using DevExpress.Utils.MVVM;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace HangBreaker.Controls {
    public sealed class Button :System.Windows.Forms.Button, ISupportCommandBinding {
        #region ISupportCommandBinding members
        IDisposable ISupportCommandBinding.BindCommand(Expression<Action> commandSelector, object source, Func<object> queryCommandParameter) {
            return CommandHelper.Bind(this, (that, action) => that.Click += (s, e) => action(), (that, getState) => that.Enabled = getState(), commandSelector, source, queryCommandParameter);
        }

        IDisposable ISupportCommandBinding.BindCommand<T>(Expression<Action<T>> commandSelector, object source, Func<T> queryCommandParameter) {
            return CommandHelper.Bind(this, (that, action) => that.Click += (s, e) => action(), (that, getState) => that.Enabled = getState(), commandSelector, source, () => queryCommandParameter == null ? default(T) : queryCommandParameter());
        }

        IDisposable ISupportCommandBinding.BindCommand(object command, Func<object> queryCommandParameter) {
            return CommandHelper.Bind(this, (that, action) => that.Click += (s, e) => action(), (that, getState) => that.Enabled = getState(), command, queryCommandParameter);
        }
        #endregion
    }
}
