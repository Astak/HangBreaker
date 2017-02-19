using DevExpress.Mvvm;
using DevExpress.Utils.MVVM.Services;
using HangBreaker.Utils;
using System;
using System.Windows.Forms;

namespace HangBreaker {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
