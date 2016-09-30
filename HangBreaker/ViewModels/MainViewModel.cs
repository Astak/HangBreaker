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

        public virtual string DisplayText { get; protected set; }
        public virtual bool IsTransparent { get; set; }

        public bool CanStart() {
            return State == ViewModelState.Initial || State == ViewModelState.PreviewOverflow;
        }

        public bool CanRestart() {
            return State == ViewModelState.Preview || State == ViewModelState.PreviewOverflow || 
                State == ViewModelState.Work || State == ViewModelState.WorkOverflow;
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
            UpdateState(ViewModelState.Preview);
        }

        public void Tick() {
            if (--Elapsed > 0) UpdateDisplayText();
            else {
                switch (State) {
                    case ViewModelState.Preview:
                        UpdateState(ViewModelState.PreviewOverflow);
                        break;
                    case ViewModelState.Work:
                        UpdateState(ViewModelState.WorkOverflow);
                        break;
                    case ViewModelState.PreviewOverflow:
                    case ViewModelState.WorkOverflow:
                        IsTransparent = !IsTransparent;
                        break;
                }
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
                    IsTransparent = true;
                    break;
                case ViewModelState.Work:
                    Elapsed = 10 * 60;
                    IsTransparent = true;
                    break;
            }
            UpdateDisplayText();
        }

        private void UpdateDisplayText() {
            switch (State) {
                case ViewModelState.Initial:
                    DisplayText = "Hello";
                    break;
                case ViewModelState.Preview:
                    DisplayText = string.Format(CultureInfo.CurrentCulture, "{0}", TimeSpan.FromSeconds(10 * 60 + Elapsed));
                    break;
                case ViewModelState.Work:
                    DisplayText = string.Format(CultureInfo.CurrentCulture, "{0}", TimeSpan.FromSeconds(Elapsed));
                    break;
                case ViewModelState.PreviewOverflow:
                case ViewModelState.WorkOverflow:
                    DisplayText = "Overtime";
                    break;
            }
        }

        private enum ViewModelState { Initial, Preview, PreviewOverflow, Work, WorkOverflow }
    }
}
