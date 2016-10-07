namespace HangBreaker {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label LabelDisplay;
            HangBreaker.Controls.Timer Timer;
            this.ButtonExit = new System.Windows.Forms.Button();
            this.ButtonRestart = new HangBreaker.Controls.Button();
            this.ButtonStart = new HangBreaker.Controls.Button();
            this.MvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            LabelDisplay = new System.Windows.Forms.Label();
            Timer = new HangBreaker.Controls.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.MvvmContext)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelDisplay
            // 
            LabelDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            LabelDisplay.Font = new System.Drawing.Font("Comic Sans MS", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            LabelDisplay.ForeColor = System.Drawing.Color.Red;
            LabelDisplay.Location = new System.Drawing.Point(0, 0);
            LabelDisplay.Name = "LabelDisplay";
            LabelDisplay.Size = new System.Drawing.Size(319, 90);
            LabelDisplay.TabIndex = 0;
            LabelDisplay.Text = "Overtime";
            LabelDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            LabelDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LabelDisplay_MouseMove);
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(275, 12);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(32, 32);
            this.ButtonExit.TabIndex = 3;
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Visible = false;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
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
            // Timer
            // 
            Timer.Interval = 1000;
            // 
            // MvvmContext
            // 
            this.MvvmContext.BindingExpressions.AddRange(new DevExpress.Utils.MVVM.BindingExpression[] {
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Restart", this.ButtonRestart),
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Start", this.ButtonStart),
            DevExpress.Utils.MVVM.BindingExpression.CreatePropertyBinding(typeof(HangBreaker.ViewModels.MainViewModel), "DisplayText", LabelDisplay, "Text"),
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Tick", Timer)});
            this.MvvmContext.ContainerControl = this;
            this.MvvmContext.ViewModelType = typeof(HangBreaker.ViewModels.MainViewModel);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(319, 90);
            this.Controls.Add(this.ButtonExit);
            this.Controls.Add(this.ButtonRestart);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(LabelDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.MvvmContext)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext MvvmContext;
        private System.Windows.Forms.Button ButtonExit;
        private Controls.Button ButtonRestart;
        private Controls.Button ButtonStart;


    }
}

