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
            DevExpress.Utils.MVVM.MVVMContext MvvmContext;
            System.Windows.Forms.Label LabelDisplay;
            System.Windows.Forms.Button ButtonStart;
            System.Windows.Forms.Button ButtonRestart;
            System.Windows.Forms.Button ButtonExit;
            MvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            LabelDisplay = new System.Windows.Forms.Label();
            ButtonStart = new System.Windows.Forms.Button();
            ButtonRestart = new System.Windows.Forms.Button();
            ButtonExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(MvvmContext)).BeginInit();
            this.SuspendLayout();
            // 
            // MvvmContext
            // 
            MvvmContext.ContainerControl = this;
            MvvmContext.ViewModelType = typeof(HangBreaker.ViewModels.MainViewModel);
            // 
            // LabelDisplay
            // 
            LabelDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            LabelDisplay.Font = new System.Drawing.Font("Comic Sans MS", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            LabelDisplay.ForeColor = System.Drawing.Color.Red;
            LabelDisplay.Location = new System.Drawing.Point(0, 0);
            LabelDisplay.Name = "LabelDisplay";
            LabelDisplay.Size = new System.Drawing.Size(500, 140);
            LabelDisplay.TabIndex = 0;
            LabelDisplay.Text = "Overtime";
            LabelDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonStart
            // 
            ButtonStart.Location = new System.Drawing.Point(12, 96);
            ButtonStart.Name = "ButtonStart";
            ButtonStart.Size = new System.Drawing.Size(32, 32);
            ButtonStart.TabIndex = 1;
            ButtonStart.UseVisualStyleBackColor = true;
            ButtonStart.Visible = false;
            // 
            // ButtonRestart
            // 
            ButtonRestart.Location = new System.Drawing.Point(50, 96);
            ButtonRestart.Name = "ButtonRestart";
            ButtonRestart.Size = new System.Drawing.Size(32, 32);
            ButtonRestart.TabIndex = 2;
            ButtonRestart.UseVisualStyleBackColor = true;
            ButtonRestart.Visible = false;
            // 
            // ButtonExit
            // 
            ButtonExit.Location = new System.Drawing.Point(456, 12);
            ButtonExit.Name = "ButtonExit";
            ButtonExit.Size = new System.Drawing.Size(32, 32);
            ButtonExit.TabIndex = 3;
            ButtonExit.UseVisualStyleBackColor = true;
            ButtonExit.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(500, 140);
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
            ((System.ComponentModel.ISupportInitialize)(MvvmContext)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


    }
}

