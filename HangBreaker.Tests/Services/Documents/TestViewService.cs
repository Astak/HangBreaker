using HangBreaker.Tests.Views;
using System;
using System.Collections.Generic;

namespace HangBreaker.Tests.Services.Documents {
    public sealed class TestViewService :IViewService {
        private IList<Func<string, TestBaseView>> ViewResolvers = new List<Func<string, TestBaseView>>();

        #region IViewService
        void IViewService.AddResolver(Func<string, TestBaseView> resolver) {
            ViewResolvers.Add(resolver);
        }

        TestBaseView IViewService.QueryView(string viewType) {
            foreach (Func<string, TestBaseView> viewResolver in ViewResolvers) {
                TestBaseView result = viewResolver(viewType);
                if (result != null) return result;
            }
            return null;
        }
        #endregion
    }

    public interface IViewService {
        void AddResolver(Func<string, TestBaseView> resolver);
        TestBaseView QueryView(string viewType);
    }
}
