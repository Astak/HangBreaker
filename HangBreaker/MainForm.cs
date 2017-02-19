using DevExpress.Utils.MVVM.Services;
using HangBreaker.Documents;
using HangBreaker.ViewModels;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HangBreaker {
    public sealed partial class MainForm : Form, IDocumentAdapterFactory {
        private Pen StartPen;
        private Pen RestartPen;
        private Pen ClosePen;

        public MainForm() {
            InitializeComponent();
            InitializeBindings();
            StartPen = new Pen(Brushes.Aqua, 5);
            StartPen.StartCap = LineCap.Round;
            StartPen.EndCap = LineCap.Round;
            RestartPen = new Pen(Brushes.Aqua, 5);
            RestartPen.StartCap = LineCap.Round;
            RestartPen.EndCap = LineCap.ArrowAnchor;
            ClosePen = new Pen(Brushes.WhiteSmoke, 4);
            ClosePen.StartCap = LineCap.Triangle;
            ClosePen.EndCap = LineCap.Triangle;
        }

        protected override void OnLoad(System.EventArgs e) {
            base.OnLoad(e);
            SetPosition();
        }

        private void InitializeBindings() {
            if (DesignMode) return;
            var api = MvvmContext.OfType<MainViewModel>();
            api.SetBinding(this, d => d.Opacity, vm => vm.IsTransparent, val => val ? .5 : 1.0);
        }

        private void SetPosition() {
            Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Right - Width - 20,
                20
                );
        }

        private void LabelDisplay_MouseMove(object sender, MouseEventArgs e) {
            ButtonStart.Visible = ButtonStart.Enabled && ButtonStart.Bounds.Contains(e.Location);
            ButtonRestart.Visible = ButtonRestart.Enabled && ButtonRestart.Bounds.Contains(e.Location);
            ButtonExit.Visible = ButtonExit.Bounds.Contains(e.Location);
        }

        private void ButtonExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void ButtonStart_Paint(object sender, PaintEventArgs e) {
            e.Graphics.FillRectangle(Brushes.AntiqueWhite, e.ClipRectangle);
            double r = e.ClipRectangle.Width / 2.0;
            int x = Convert.ToInt32(Math.Round(e.ClipRectangle.Left + r - r * Math.Sin(Math.PI / 3)));
            double h = 3 * r / Math.Sqrt(3) / 2.0;
            double ox = e.ClipRectangle.Top + r;
            int y1 = Convert.ToInt32(Math.Round(ox - h));
            int y2 = Convert.ToInt32(Math.Round(ox + h));
            var points = new Point[] {
                new Point(x, y1),
                new Point(e.ClipRectangle.Right, Convert.ToInt32(Math.Round(ox))),
                new Point(x, y2)
            };
            e.Graphics.FillPolygon(Brushes.Aqua, points);
        }

        private void ButtonRestart_Paint(object sender, PaintEventArgs e) {
            e.Graphics.FillRectangle(Brushes.AntiqueWhite, e.ClipRectangle);
            var arcBounds = e.ClipRectangle;
            arcBounds.Inflate(-5, -5);
            e.Graphics.DrawArc(RestartPen, arcBounds, 300, 300);
        }

        private void ButtonExit_Paint(object sender, PaintEventArgs e) {
            e.Graphics.FillRectangle(Brushes.AntiqueWhite, e.ClipRectangle);
            var buttonRect = e.ClipRectangle;
            buttonRect.Inflate(-4, -4);
            e.Graphics.FillEllipse(Brushes.IndianRed, buttonRect);
            var centerPoint = new Point(e.ClipRectangle.Left + e.ClipRectangle.Width / 2, e.ClipRectangle.Top + e.ClipRectangle.Height / 2);
            int crossSize = 5;
            e.Graphics.DrawLine(ClosePen, centerPoint.X - crossSize, centerPoint.Y - crossSize, centerPoint.X + crossSize, centerPoint.Y + crossSize);
            e.Graphics.DrawLine(ClosePen, centerPoint.X + crossSize, centerPoint.Y - crossSize, centerPoint.X - crossSize, centerPoint.Y + crossSize);
        }
        #region IDocumentAdapterFactory
        IDocumentAdapter IDocumentAdapterFactory.Create() => new UserControlDocumentAdapter(this);
        #endregion
    }
}
