using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Testing
{
    class FilterImageTesting
    {
        public static void Median()
        {
            string s = Environment.CurrentDirectory;
            var image = new Bitmap(s + "\\1.jpg", true);
            FilterImage.Median(ref image);
            image.Save("2.jpg");
        }
        public static void Monochrome()
        {
            string s = Environment.CurrentDirectory;
            var image = new Bitmap(s + "\\1.jpg", true);
            FilterImage.Monochrome(ref image, 130);
            image.Save("3.jpg");
        }
        public static void Scale()
        {
            string s = Environment.CurrentDirectory;
            var image = new Bitmap(s + "\\1.jpg", true);
            Bitmap bmp = new Bitmap(image, 2 * image.Width, 2 * image.Height);
            Bitmap bmp2 = new Bitmap(bmp, bmp.Width / 2, bmp.Height / 2);
            bmp2.Save("5.jpg");
        }
    }
}
