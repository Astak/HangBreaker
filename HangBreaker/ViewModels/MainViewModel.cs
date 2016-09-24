using DevExpress.Mvvm.POCO;
using System;
using System.Globalization;

namespace HangBreaker.ViewModels {
    public class MainViewModel {
        private ViewModelState State;
        private int Elapsed;

        public MainViewModel() {
            DisplayText = "Hello";
        }

        public virtual string DisplayText { get; set; }

        public bool CanStart() {
            return State == ViewModelState.Initial || State == ViewModelState.PreviewOverflow;
        }

        public bool CanRestart() {
            return State == ViewModelState.Preview || State == ViewModelState.PreviewOverflow || 
                State == ViewModelState.Work || State == ViewModelState.WorkOverflow;
        }

        public bool CanTick() {
            return State == ViewModelState.Preview || State == ViewModelState.Work;
        }

        public void Start() {
            switch (State) {
                case ViewModelState.Initial:
                    UpdateState(ViewModelState.Preview);
                    break;
                case ViewModelState.PreviewOverflow:
                    UpdateState(ViewModelState.Work);
                    break;
            }
        }

        public void Restart() {
            throw new System.NotImplementedException();
        }

        public void Tick() {
            if (!(State == ViewModelState.Preview || State == ViewModelState.Work)) return;
            if (--Elapsed > 0) return;
            switch (State) {
                case ViewModelState.Preview:
                    UpdateState(ViewModelState.PreviewOverflow);
                    break;
                case ViewModelState.Work:
                    UpdateState(ViewModelState.WorkOverflow);
                    break;
            }
        }

        private void UpdateState(ViewModelState state) {
            State = state;
            this.RaiseCanExecuteChanged(vm => vm.Start());
            this.RaiseCanExecuteChanged(vm => vm.Restart());
            this.RaiseCanExecuteChanged(vm => vm.Tick());
            switch (State) {
                case ViewModelState.Preview:
                    Elapsed = 5 * 60;
                    break;
                case ViewModelState.Work:
                    Elapsed = 10 * 60;
                    break;
            }
            UpdateDisplayText();
        }

        private void UpdateDisplayText() {
            DisplayText = string.Format(CultureInfo.CurrentUICulture, "{0}", TimeSpan.FromSeconds(10 * 60 + Elapsed));
        }

        private enum ViewModelState { Initial, Preview, PreviewOverflow, Work, WorkOverflow }
    }
}
