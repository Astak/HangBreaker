namespace HangBreaker.ViewModels {
    public class MainViewModel {
        private ViewModelState State;
        private int Elapsed;

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
                    State = ViewModelState.Preview;
                    break;
                case ViewModelState.PreviewOverflow:
                    State = ViewModelState.Work;
                    break;
            }
        }

        private enum ViewModelState { Initial, Preview, PreviewOverflow, Work, WorkOverflow }

        public void Tick() {
            if (!(State == ViewModelState.Preview || State == ViewModelState.Work)) return;
            if (--Elapsed > 0) return;
            switch (State) {
                case ViewModelState.Preview:
                    State = ViewModelState.PreviewOverflow;
                    break;
                case ViewModelState.Work:
                    State = ViewModelState.WorkOverflow;
                    break;
            }
        }
    }
}
