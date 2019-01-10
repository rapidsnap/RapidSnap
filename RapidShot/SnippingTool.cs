using RapidSnap.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RapidShot
{
    public class SnippingTool
    {
        public bool IsSnapping { get; private set; }

        private List<Canvas> _canvases;

        public SnippingTool() { }

        public void TakeSnap()
        {
            IsSnapping = true;

            _canvases = new List<Canvas>();

            foreach (var screen in Screen.AllScreens)
            {
                var canvas = new Canvas()
                {
                    Left = screen.WorkingArea.Left,
                    Top = screen.WorkingArea.Top
                };

                canvas.Show();
                canvas.SnapTaken += OnSnapTaken;

                _canvases.Add(canvas);
            }

        }

        public void OnSnapTaken(object sender, SnapEventArgs e)
        {
            foreach (var canvas in _canvases)
            {
                canvas.Hide();
                canvas.Dispose();
                canvas.Close();
            }

            IsSnapping = false;

            if (e.Image == null)
                return;

            SaveToClipboard(e.Image);
        }
        
        public void TakeScreenShot()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            SaveToClipboard(bitmap);

            bitmap.Dispose();
            graphics.Dispose();
        }

        private void SaveToClipboard(Image image)
        {
            Clipboard.SetImage(image);
            MenuForm.MinimizeFootprint();
        }
    }
}
