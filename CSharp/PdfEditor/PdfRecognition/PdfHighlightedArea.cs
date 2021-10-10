using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace PdfRecognition
{
    class PdfHighlightedArea
    {
        public static bool CompareColor(Color A, Color B, int Level)
        {
            return ((Math.Abs(A.R - B.R) < Level) && (Math.Abs(A.G - B.G) < Level) && (Math.Abs(A.B - B.B) < Level));
        }

        public static void FindAreasColor(Color BkGround, Color AreaColor, Color TextColor, int level, ref Bitmap btmp, int depth)
        {
            for (int i = 0; i < btmp.Height; i++)
            {
                for (int j = 0; j < btmp.Width - 1; j++)

                {
                    var color = btmp.GetPixel(j, i);
                    if (CompareColor(color, AreaColor, level) || CompareColor(color, TextColor, level))
                    {

                    }
                    else {
                        btmp.SetPixel(j, i, BkGround);
                    }
                }
            }


        }
        public static Bitmap GetBitmap(Bitmap btmp, int x1, int y1, int x2, int y2)
        {
            Rectangle cloneRect = new Rectangle(x1, y1, x2, y2);
            return btmp.Clone(cloneRect, btmp.PixelFormat);
        }
        public static bool IsAreaNeighbour(Bitmap btmp, int i, int j, int depth, Color AreaColor, int level)
        {
            bool IsAreasColorPoint = false;
            int Mx = Math.Min(i + depth, btmp.Width - 1), My = Math.Min(j + depth, btmp.Height - 1);
            for (int x = Math.Max(0, (i - depth)); x <= Mx; x++)
            {
                for (int y = Math.Max(0, j - depth); y <= My; y++)
                {
                    var color = btmp.GetPixel(x, y);
                    if (CompareColor(color, AreaColor, level)) { IsAreasColorPoint = true; }

                }
            }
            return IsAreasColorPoint;
        }
        public static void FindHightlighted(Color BkGround, Color AreaColor, int level, ref Bitmap btmp, int depth)
        {
            Queue<Point> Q = new Queue<Point>();
            Point p = new Point(0, 0);
            Q.Enqueue(p);
            bool[,] IsVisited = new bool[btmp.Width, btmp.Height];
            IsVisited[0, 0] = true;
            for (int i = 0; i < btmp.Width; i++)
            {
                for (int j = 0; j < btmp.Height; j++)
                    IsVisited[i, j] = false;
            }
            while (Q.Count > 0)
            {
                Point c = Q.Dequeue();
                int x = c.x, y = c.y;
                if (CompareColor(btmp.GetPixel(x, y), AreaColor, level))
                {

                }
                else
                {
                    bool b = false;
                    if (depth > 0)
                    {
                        b = IsAreaNeighbour(btmp, x, y, depth, AreaColor, level);
                    }
                    if (b == false)
                    {
                        btmp.SetPixel(x, y, BkGround);
                        int[] dx = new int[4] { 0, 0, -1, 1 };
                        int[] dy = new int[4] { 1, -1, 0, 0 };
                        for (int i = 0; i < 4; i++)
                        {
                            int nx = x + dx[i], ny = y + dy[i];
                            try
                            {
                                if (!IsVisited[nx, ny])
                                {
                                    IsVisited[nx, ny] = true;
                                    Q.Enqueue(new Point(nx, ny));
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {
                            }
                        }
                    }
                }
            }
        }
        public static void FindAllHighLighted(string path, Color BkGround, Color AreaColor, int level, int depth)
        {
            Directory.SetCurrentDirectory(path);
            string[] names = Directory.GetFiles(path, "*.png");

            for (int i = 0; i < names.Length; i++)
            {
                var image = new Bitmap(names[i], true);
                PdfHighlightedArea.FindHightlighted(BkGround, AreaColor, level, ref image, depth);
                string[] str = names[i].Split('.');
                image.Save(str[0] + "_Step2" + ".png");
                image.Dispose();
                File.Delete(names[i]);
            }
        }
        public static void FindAllHighLightedToText(string path, Color BkGround, Color AreaColor, int level, int depth)
        {
            Directory.SetCurrentDirectory(path);
            string[] names = Directory.GetFiles(path, "*.png");
            for (int i = 0; i < names.Length; i++)
            {
                var image = new Bitmap(names[i], true);
                PdfHighlightedArea.FindHightlighted(BkGround, AreaColor, level, ref image, depth);
                string[] str = names[i].Split('.');
                names[i] = str[0] + "_Step2" + ".png";
                image.Save(names[i]);
                image = null;

            }
            //using MODI; for text recognition
            
            //for (int i = 0; i < names.Length; i++)
            //{
            //    MODI.Document modiDocument = new MODI.Document();
            //    modiDocument.Create(names[i]);
            //    modiDocument.OCR(MiLANGUAGES.miLANG_ENGLISH);
            //    MODI.Image modiImage = (modiDocument.Images[0] as MODI.Image);
            //    string extractedText = modiImage.Layout.Text;
            //    modiDocument.Close();
            //    Console.WriteLine(extractedText);
            //}
        }
    }
}
