using System;
using System.Drawing;

namespace ImageEditor
{
    /// <summary>
    /// This class provides some filters for images
    /// </summary>
    class FilterImage
    {
        /// <summary>
        /// Blurs an image
        /// </summary>
        /// <param name="image">The image that will be processed</param>
        public static void Median(ref Bitmap image)
        {
            var arrR = new int[8];
            var arrG = new int[8];
            var arrB = new int[8];
            var outImage = new Bitmap(image);

            for (int i = 1; i < image.Width - 1; i++)
                for (int j = 1; j < image.Height - 1; j++)
                {
                    for (int i1 = 0; i1 < 2; i1++)
                        for (int j1 = 0; j1 < 2; j1++)
                        {
                            var p = image.GetPixel(i + i1 - 1, j + j1 - 1);

                            arrR[i1 * 3 + j1] = ((p.R + p.G + p.B) / 3) & 0xff;
                            arrG[i1 * 3 + j1] = ((p.R + p.G + p.B) / 3) >> 8 & 0xff;
                            arrB[i1 * 3 + j1] = ((p.R + p.G + p.B) / 3) >> 16 & 0xff;
                        }
                    Array.Sort(arrR);
                    Array.Sort(arrG);
                    Array.Sort(arrB);

                    outImage.SetPixel(i, j, Color.FromArgb(arrR[3], arrG[4], arrB[5]));
                }

            image = outImage;
        }
        /// <summary>
        /// Converts image into black-white
        /// </summary>
        /// <param name="image">The image that will be processed</param>
        /// <param name="level">The level of contrast</param>
        public static void Monochrome(ref Bitmap image, int level)
        {
            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    var color = image.GetPixel(i, j);
                    int sr = (color.R + color.G + color.B) / 3;
                    image.SetPixel(i, j, (sr < level ? Color.Black : Color.White));
                }
            }
        }
    }
}
