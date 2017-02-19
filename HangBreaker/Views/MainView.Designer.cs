namespace HangBreaker.Views {
    partial class MainView {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.LabelDisplay = new System.Windows.Forms.Label();
            this.Timer = new HangBreaker.Controls.Timer();
            this.ButtonExit = new HangBreaker.Controls.Button();
            this.ButtonRestart = new HangBreaker.Controls.Button();
            this.ButtonStart = new HangBreaker.Controls.Button();
            this.MvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MvvmContext)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelDisplay
            // 
            this.LabelDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelDisplay.Font = new System.Drawing.Font("Comic Sans MS", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelDisplay.ForeColor = System.Drawing.Color.Red;
            this.LabelDisplay.Location = new System.Drawing.Point(0, 0);
            this.LabelDisplay.Name = "LabelDisplay";
            this.LabelDisplay.Size = new System.Drawing.Size(324, 85);
            this.LabelDisplay.TabIndex = 0;
            this.LabelDisplay.Text = "Overtime";
            this.LabelDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LabelDisplay_MouseMove);
            // 
            // Timer
            // 
            this.Timer.Interval = 1000;
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(275, 12);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(32, 32);
            this.ButtonExit.TabIndex = 3;
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Visible = false;
            this.ButtonExit.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonExit_Paint);
            // 
            // ButtonRestart
            // 
            this.ButtonRestart.Location = new System.Drawing.Point(50, 46);
            this.ButtonRestart.Name = "ButtonRestart";
            this.ButtonRestart.Size = new System.Drawing.Size(32, 32);
            this.ButtonRestart.TabIndex = 2;
            this.ButtonRestart.UseVisualStyleBackColor = true;
            this.ButtonRestart.Visible = false;
            this.ButtonRestart.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonRestart_Paint);
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(12, 46);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(32, 32);
            this.ButtonStart.TabIndex = 1;
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Visible = false;
            this.ButtonStart.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonStart_Paint);
            // 
            // MvvmContext
            // 
            this.MvvmContext.BindingExpressions.AddRange(new DevExpress.Utils.MVVM.BindingExpression[] {
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Restart", this.ButtonRestart),
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Start", this.ButtonStart),
            DevExpress.Utils.MVVM.BindingExpression.CreatePropertyBinding(typeof(HangBreaker.ViewModels.MainViewModel), "DisplayText", this.LabelDisplay, "Text"),
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Tick", this.Timer),
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Close", this.ButtonExit)});
            this.MvvmContext.ContainerControl = this;
            this.MvvmContext.ViewModelType = typeof(HangBreaker.ViewModels.MainViewModel);
            // 
            // MainView
            // 
            this.Controls.Add(this.ButtonExit);
            this.Controls.Add(this.ButtonRestart);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.LabelDisplay);
            this.Name = "MainView";
            this.Size = new System.Drawing.Size(324, 85);
            ((System.ComponentModel.ISupportInitialize)(this.MvvmContext)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext MvvmContext;
        private Controls.Button ButtonExit;
        private Controls.Button ButtonRestart;
        private Controls.Button ButtonStart;
        private System.Windows.Forms.Label LabelDisplay;
        private Controls.Timer Timer;
    }
}
