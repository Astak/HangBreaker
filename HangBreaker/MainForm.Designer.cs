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
            System.Windows.Forms.Button ButtonExit;
            HangBreaker.Controls.Button ButtonRestart;
            HangBreaker.Controls.Button ButtonStart;
            HangBreaker.Controls.Timer Timer;
            this.MvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            LabelDisplay = new System.Windows.Forms.Label();
            ButtonExit = new System.Windows.Forms.Button();
            ButtonRestart = new HangBreaker.Controls.Button();
            ButtonStart = new HangBreaker.Controls.Button();
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
            // 
            // ButtonExit
            // 
            ButtonExit.Location = new System.Drawing.Point(275, 12);
            ButtonExit.Name = "ButtonExit";
            ButtonExit.Size = new System.Drawing.Size(32, 32);
            ButtonExit.TabIndex = 3;
            ButtonExit.UseVisualStyleBackColor = true;
            ButtonExit.Visible = false;
            // 
            // ButtonRestart
            // 
            ButtonRestart.Location = new System.Drawing.Point(50, 46);
            ButtonRestart.Name = "ButtonRestart";
            ButtonRestart.Size = new System.Drawing.Size(32, 32);
            ButtonRestart.TabIndex = 2;
            ButtonRestart.UseVisualStyleBackColor = true;
            ButtonRestart.Visible = false;
            // 
            // ButtonStart
            // 
            ButtonStart.Location = new System.Drawing.Point(12, 46);
            ButtonStart.Name = "ButtonStart";
            ButtonStart.Size = new System.Drawing.Size(32, 32);
            ButtonStart.TabIndex = 1;
            ButtonStart.UseVisualStyleBackColor = true;
            ButtonStart.Visible = false;
            // 
            // MvvmContext
            // 
            this.MvvmContext.BindingExpressions.AddRange(new DevExpress.Utils.MVVM.BindingExpression[] {
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Restart", ButtonRestart),
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Start", ButtonStart),
            DevExpress.Utils.MVVM.BindingExpression.CreatePropertyBinding(typeof(HangBreaker.ViewModels.MainViewModel), "DisplayText", LabelDisplay, "Text"),
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(HangBreaker.ViewModels.MainViewModel), "Tick", Timer)});
            this.MvvmContext.ContainerControl = this;
            this.MvvmContext.ViewModelType = typeof(HangBreaker.ViewModels.MainViewModel);
            // 
            // Timer
            // 
            Timer.Interval = 1000;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(319, 90);
            this.Controls.Add(ButtonExit);
            this.Controls.Add(ButtonRestart);
            this.Controls.Add(ButtonStart);
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


    }
}

