using RapidSnap.Forms;
using RapidSnap.Properties;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

            SaveScreenshot(e.Image);
        }
        
        public void TakeScreenshot()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            SaveScreenshot(bitmap);

            bitmap.Dispose();
            graphics.Dispose();
        }

        private void SaveScreenshot(Image image)
        {
            if (Settings.Default.SaveToClipboard)
                SaveToClipboard(image);
            else
                SaveToDisk(image);
        }

        private void SaveToClipboard(Image image)
        {
            Clipboard.SetImage(image);
            MenuForm.MinimizeFootprint();
        }

        public void SaveToDisk(Image image)
        {
            var fileDiag = new SaveFileDialog()
            {
                Title = "Save As",
                FileName = "Unkown",
                Filter = "PNG |*.png|GIF |*.gif|Bitmap |*.bmp|JPEG |*.jpg",
            };

            fileDiag.FileOk += (s, e) =>
            {
                if (fileDiag.FileName == "")
                    return;

                var fileStream = fileDiag.OpenFile();
                switch (fileDiag.FilterIndex)
                {
                    case 1: image.Save(fileStream, ImageFormat.Png); break;
                    case 2: image.Save(fileStream, ImageFormat.Gif); break;
                    case 3: image.Save(fileStream, ImageFormat.Bmp); break;
                    case 4: image.Save(fileStream, ImageFormat.Jpeg); break;
                }

                fileStream.Close();
            };

            fileDiag.ShowDialog();
        }
    }
}
