using ImageMapModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageMapGame
{
    public partial class Main : Form
    {
        ImageMap ImageMap;
        Pen Pen = new Pen(Color.Red, 1);
        public Main()
        {
            InitializeComponent();
            Pen.DashPattern = new float[] { 5, 5 };
        }

        private void btnUploadImageMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(*.json)|*.json";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var json = File.ReadAllText(open.FileName);
                ImageMap = JsonSerializer.Deserialize<ImageMap>(json);
                pbMain.Image = new Bitmap(ImageMap.ImageUrl);

                Bitmap pbImageBitmap = (Bitmap)(pbMain.Image);
                Graphics graphics = Graphics.FromImage((Image)pbImageBitmap);
                foreach (var io in ImageMap.ImageObjects)
                {
                    graphics.DrawRectangle(Pen, io.Rectangle);
                }
                pbMain.Refresh();
                graphics.Dispose();

            }
        }

        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var io in ImageMap.ImageObjects)
            {
                //if (io.Rectangle.Contains(e.Location))
                if (io.Rectangle.Contains(UnscaledCursor(e.Location)))
                {
                    lblObjectName.Text = io.Name;
                    if (io.SoundUrl != null)
                    {
                        if (File.Exists(io.SoundUrl))
                        {
                            playSoundMP3(io.SoundUrl);
                        }
                    }
                    break;
                }
            }
        }
        private void playSound(string path)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = path;
            player.Load();
            player.Play();
        }
        private void playSoundMP3(string path)
        {
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = path;
            wplayer.controls.play();
        }
        private Point UnscaledCursor(Point p)
        {
            //Point p = pbMain.PointToClient(Cursor.Position);
            Point unscaled_p = new Point();

            // image and container dimensions
            int w_i = pbMain.Image.Width;
            int h_i = pbMain.Image.Height;
            int w_c = pbMain.Width;
            int h_c = pbMain.Height;

            float imageRatio = w_i / (float)h_i; // image W:H ratio
            float containerRatio = w_c / (float)h_c; // container W:H ratio

            if (imageRatio >= containerRatio)
            {
                // horizontal image
                float scaleFactor = w_c / (float)w_i;
                float scaledHeight = h_i * scaleFactor;
                // calculate gap between top of container and top of image
                float filler = Math.Abs(h_c - scaledHeight) / 2;
                unscaled_p.X = (int)(p.X / scaleFactor);
                unscaled_p.Y = (int)((p.Y - filler) / scaleFactor);
            }
            else
            {
                // vertical image
                float scaleFactor = h_c / (float)h_i;
                float scaledWidth = w_i * scaleFactor;
                float filler = Math.Abs(w_c - scaledWidth) / 2;
                unscaled_p.X = (int)((p.X - filler) / scaleFactor);
                unscaled_p.Y = (int)(p.Y / scaleFactor);
            }

            return unscaled_p;

        }
    }
}
