using ImageMapModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageLabelling
{
    public partial class Main : Form
    {
        Point start;
        bool blnDraw;
        Rectangle rect;
        ImageMap ImageMap;
        Pen Pen = new Pen(Color.Red, 3);
        SolidBrush TextBrush = new SolidBrush(Color.Black);
        int TextSize = 20;
        Font TextFont = new Font("Arial", 10);
        SolidBrush Brush = new SolidBrush(Color.White);

        int MaxHeight = 700;
        Bitmap Bitmap;

        public Main()
        {
            InitializeComponent();
            ImageMap = new ImageMap();
        }


        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
            Invalidate();
            blnDraw = true;
            pbMain.Refresh();
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            if (blnDraw)
            {
                if (pbMain.Image != null)
                {
                    if (rect != null && rect.Width > 0 && rect.Height > 0)
                    {
                        e.Graphics.DrawRectangle(Pen, rect);
                        
                    }
                }
            }
        }

        private void pbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (blnDraw)
            {
                if (e.Button != MouseButtons.Left)
                    return;
                Point tempEndPoint = e.Location;
                rect.Location = new Point(
                    Math.Min(start.X, tempEndPoint.X),
                    Math.Min(start.Y, tempEndPoint.Y));
                rect.Size = new Size(
                    Math.Abs(start.X - tempEndPoint.X),
                    Math.Abs(start.Y - tempEndPoint.Y));
                pbMain.Invalidate();
            }
        }
        private void DrawRectangle(Graphics graphics, Rectangle rect, string lbl)
        {
            graphics.DrawRectangle(Pen, rect);
            Rectangle labelRect = new Rectangle(rect.Location, new Size((int)(0.8 * TextSize) * lbl.Length, TextSize));
            graphics.FillRectangle(Brush, labelRect);
            graphics.DrawString(lbl, TextFont, TextBrush, labelRect);
        }
        private void pbMain_MouseUp(object sender, MouseEventArgs e)
        {
            blnDraw = false;

            Bitmap pbImageBitmap = (Bitmap)(pbMain.Image);
            Graphics graphics = Graphics.FromImage((Image)pbImageBitmap);
            if (rect != null && rect.Width > 0 && rect.Height > 0)
                DrawRectangle(graphics, rect, tbLabel.Text);
            pbMain.Refresh();
            graphics.Dispose();
            ImageMap.ImageObjects.Add(new ImageObject() { Name = tbLabel.Text, Rectangle = rect });
        }



        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                rect.Width = rect.Height = 0;
                ImageMap = new ImageMap();
                ImageMap.ImageUrl = open.FileName;
                var original = new Bitmap(open.FileName);
                int width = (int)(MaxHeight * original.Width / original.Height);
                Bitmap = new Bitmap(original, new Size(width, MaxHeight));
                pbMain.Refresh();
                pbMain.Image = Bitmap;
                Bitmap = (Bitmap)Bitmap.Clone();
                pbMain.Refresh();
                
            }
        }

        private void btnUploadImageMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(*.json)|*.json";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var json = File.ReadAllText(open.FileName);
                ImageMap = JsonSerializer.Deserialize<ImageMap>(json);
                tbName.Text = ImageMap.Name;
                pbMain.Image = new Bitmap(ImageMap.ImageUrl);

                Bitmap pbImageBitmap = (Bitmap)(pbMain.Image);
                Graphics graphics = Graphics.FromImage((Image)pbImageBitmap);
                foreach(var io in ImageMap.ImageObjects)
                {
                    DrawRectangle(graphics, io.Rectangle, io.Name);
                }
                pbMain.Refresh();
                graphics.Dispose();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //SaveFileDialog save = new SaveFileDialog();
            //save.Filter = "(*.json)|*.json";
            //if (save.ShowDialog() == DialogResult.Cancel)
            //    return;
            //string path = save.FileName;

            // save image
            ImageMap.Name = tbName.Text;
            string imagePath = Path.Combine("data", "img", ImageMap.Name + ".jpg");
            ImageMap.ImageUrl = imagePath;
            if (!(File.Exists(imagePath)))
                Bitmap.Save(imagePath, ImageFormat.Jpeg);

            string path = Path.Combine("data", ImageMap.Name + ".json");

            
            string json = JsonSerializer.Serialize(ImageMap);
            File.WriteAllText(path, json);

        }
    }
}
