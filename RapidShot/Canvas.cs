using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace RapidShot
{
    public class SnapEventArgs : EventArgs
    {
        public Image Image { get; set; }
    }

    public class Canvas : Form
    {
        private Point _mouseStartPos, _mouseCurrentPos;
        private bool _drawing;

        public event EventHandler<SnapEventArgs> SnapTaken;

        public Canvas()
        {
            SuspendLayout();
            ResumeLayout(false);
            
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            BackColor = Color.Black;
            Opacity = 0.2;
            TransparencyKey = Color.White;
            Cursor = Cursors.Cross;
            DoubleBuffered = true;
            ShowInTaskbar = false;

            KeyDown += OnKeyDown;
            MouseDown += OnMouseClick;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseRelease;
            Paint += OnPaint;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                OnSnapTaken(null);
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (_drawing)
                return;

            _mouseStartPos = _mouseCurrentPos = e.Location;
            _drawing = true;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_drawing)
                return;
            
            _mouseCurrentPos = e.Location;
            Invalidate();
        }

        private void OnMouseRelease(object sender, MouseEventArgs e)
        {
            if (_mouseStartPos.X == e.Location.X || _mouseStartPos.Y == e.Location.Y)
            {
                OnSnapTaken(null);
                return;
            }

            var rectangle = GetRectangle(PointToScreen(_mouseStartPos), PointToScreen(e.Location));

            using (var image = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format24bppRgb))
            {
                using (var g = Graphics.FromImage(image))
                {
                    g.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);

                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                }

                OnSnapTaken(image);
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (!_drawing)
                return;

            var rectangle = GetRectangle(_mouseStartPos, _mouseCurrentPos);

            var region = new Region(ClientRectangle);
            region.Intersect(rectangle);

            e.Graphics.FillRegion(Brushes.White, region);
            e.Graphics.DrawRectangle(Pens.Red, rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 1, rectangle.Height + 1);
        }

        protected virtual void OnSnapTaken(Image image)
        {
            if (SnapTaken == null)
                return;

            SnapTaken(this, new SnapEventArgs() { Image = image });
        }

        public Rectangle GetRectangle(Point start, Point end)
            => new Rectangle(
                (int) Math.Min(start.X, end.X),
                (int) Math.Min(start.Y, end.Y),
                (int) Math.Abs(start.X - end.X),
                (int) Math.Abs(start.Y - end.Y));
    }
}
