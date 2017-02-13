using DevExpress.Mvvm;
using HangBreaker.Utils;
using DevExpress.Mvvm.POCO;
using System;
using System.Globalization;
using DevExpress.Xpo;
using HangBreaker.Services;
using HangBreaker.BusinessModel;

namespace HangBreaker.ViewModels {
    public class MainViewModel {
        private IViewModelState State;
        private int Elapsed;
        private int WorkSessionKey;

        public MainViewModel() {
            DisplayText = "Hello";
            State = new InitialViewModelState(this);
        }

        public virtual string DisplayText { get; protected set; }
        public virtual bool IsTransparent { get; set; }

        private IDocumentManagerService DocumentManagerService {
            get { return ServiceContainer.Default.GetService<IDocumentManagerService>(Constants.ServiceKey); }
        }

        protected virtual IXpoService XpoService {
            get { throw new System.NotImplementedException(); }
        }

        public bool CanStart() {
            return State.CanStart;
        }

        public bool CanRestart() {
            return State.CanRestart;
        }

        public void Start() {
            State.Start();
        }

        private void CreateSessionAsync(Action callback) {
            using (UnitOfWork uow = XpoService.GetUnitOfWork()) {
                var workSession = new WorkSession(uow);
                uow.CommitChanges();
                WorkSessionKey = workSession.Oid;
            }
            IDocument document = DocumentManagerService.CreateDocument(Constants.StartSessionViewName, WorkSessionKey, null);
            document.Show();
            ActiveDocumentChangedEventHandler handler = null;
            handler = (s, e) => {
                DocumentManagerService.ActiveDocumentChanged -= handler;
                callback();
            };
            DocumentManagerService.ActiveDocumentChanged += handler;
        }

        public void Restart() {
            IDocument document = DocumentManagerService.CreateDocument(Constants.SetStatusViewName, WorkSessionKey, null);
            document.Show();
            DocumentManagerService.ActiveDocumentChanged += OnActiveDocumentChangedAfterSetStatus;
        }

        private void OnActiveDocumentChangedAfterSetStatus(object sender, ActiveDocumentChangedEventArgs e) {
            DocumentManagerService.ActiveDocumentChanged -= OnActiveDocumentChangedAfterSetStatus;
            using (UnitOfWork uow = XpoService.GetUnitOfWork()) {
                var workSession = new WorkSession(uow);
                uow.CommitChanges();
                WorkSessionKey = workSession.Oid;
            }
            IDocument document = DocumentManagerService.CreateDocument(Constants.StartSessionViewName, WorkSessionKey, null);
            document.Show();
            DocumentManagerService.ActiveDocumentChanged += OnActiveDocumentChangedAfterStartSession;
        }

        private void OnActiveDocumentChangedAfterStartSession(object sender, ActiveDocumentChangedEventArgs e) {
            DocumentManagerService.ActiveDocumentChanged -= OnActiveDocumentChangedAfterStartSession;
            UpdateState(new PreviewViewModelState(this));
        }

        public void Tick() {
            if (--Elapsed > 0) UpdateDisplayText();
            else State.OnElapsed();
        }

        public void Close() {
            this.CloseDocument();
        }

        private void UpdateState(ViewModelState state) {
            State = state;
            this.RaiseCanExecuteChanged(vm => vm.Start());
            this.RaiseCanExecuteChanged(vm => vm.Restart());
            this.RaiseCanExecuteChanged(vm => vm.Tick());
            State.Update();
            UpdateDisplayText();
        }

        private void UpdateDisplayText() {
            DisplayText = State.DisplayText;
        }

        private interface IViewModelState {
            bool CanStart { get; }
            bool CanRestart { get; }
            string DisplayText { get; }
            void Start();
            void OnElapsed();
            void Update();
        }

        private abstract class ViewModelState : IViewModelState {
            protected MainViewModel ViewModel;

            public ViewModelState(MainViewModel viewModel) {
                this.ViewModel = viewModel;
            }

            protected abstract bool CanStart { get; }
            protected abstract bool CanRestart { get; }
            protected abstract string DisplayText { get; }
            protected abstract void Start();
            protected abstract void OnElapsed();
            protected abstract void Update();

            bool IViewModelState.CanStart {
                get { return CanStart; }
            }

            bool IViewModelState.CanRestart {
                get { return CanRestart; }
            }

            string IViewModelState.DisplayText {
                get { return DisplayText; }
            }

            void IViewModelState.Start() {
                Start();
            }

            void IViewModelState.OnElapsed() {
                OnElapsed();
            }

            void IViewModelState.Update() {
                Update();
            }
        }

        private class InitialViewModelState : ViewModelState {
            public InitialViewModelState(MainViewModel viewModel) : base(viewModel) { }

            protected override bool CanStart {
                get { return true; }
            }

            protected override bool CanRestart {
                get { return false; }
            }

            protected override string DisplayText {
                get { return "Hello"; }
            }

            protected override void Start() {
                ViewModel.CreateSessionAsync(() => ViewModel.UpdateState(new PreviewViewModelState(ViewModel)));
            }

            protected override void OnElapsed() { }

            protected override void Update() { }
        }

        private class PreviewViewModelState : ViewModelState {
            public PreviewViewModelState(MainViewModel viewModel) : base(viewModel) { }

            protected override bool CanStart {
                get { return false; }
            }

            protected override bool CanRestart {
                get { return true; }
            }

            protected override string DisplayText {
                get { return string.Format(CultureInfo.CurrentCulture, "{0}", TimeSpan.FromSeconds(10 * 60 + ViewModel.Elapsed)); }
            }

            protected override void Start() { }

            protected override void OnElapsed() {
                ViewModel.UpdateState(new PreviewOverflowViewModelState(ViewModel));
            }

            protected override void Update() {
                ViewModel.Elapsed = 5 * 60;
                ViewModel.IsTransparent = true;
            }
        }

        private class PreviewOverflowViewModelState : ViewModelState {
            public PreviewOverflowViewModelState(MainViewModel viewModel) : base(viewModel) { }

            protected override bool CanStart {
                get { return true; }
            }

            protected override bool CanRestart {
                get { return true; }
            }

            protected override string DisplayText {
                get { return "Overtime"; }
            }

            protected override void Start() {
                ViewModel.UpdateState(new WorkViewModelState(ViewModel));
            }

            protected override void OnElapsed() {
                ViewModel.IsTransparent = !ViewModel.IsTransparent;
            }

            protected override void Update() { }
        }

        private class WorkViewModelState : ViewModelState {
            public WorkViewModelState(MainViewModel viewModel) : base(viewModel) { }

            protected override bool CanStart {
                get { return false; }
            }

            protected override bool CanRestart {
                get { return true; }
            }

            protected override string DisplayText {
                get { return string.Format(CultureInfo.CurrentCulture, "{0}", TimeSpan.FromSeconds(ViewModel.Elapsed)); }
            }

            protected override void Start() { }

            protected override void OnElapsed() {
                ViewModel.UpdateState(new WorkOverflowViewModelState(ViewModel));
            }

            protected override void Update() {
                ViewModel.Elapsed = 10 * 60;
                ViewModel.IsTransparent = true;
            }
        }

        private class WorkOverflowViewModelState : ViewModelState {
            public WorkOverflowViewModelState(MainViewModel viewModel) : base(viewModel) { }

            protected override bool CanStart {
                get { return false; }
            }

            protected override bool CanRestart {
                get { return true; }
            }

            protected override string DisplayText {
                get { return "Overtime"; }
            }

            protected override void Start() { }

            protected override void OnElapsed() {
                ViewModel.IsTransparent = !ViewModel.IsTransparent;
            }

            protected override void Update() { }
        }
    }
}
