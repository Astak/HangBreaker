using System.Windows.Forms;

namespace HangBreaker.Services {
    public interface IDialogService {
        DialogResult ShowDialog(UserControl view);
        void HideDialog(DialogResult result);
    }
}
