using System;

namespace Patterns
{
    public class Text :ICloneable
    {
        public Text(string font, int size, string color)
        {
            Font = font;
            Size = size;
            Color = color;
        }
        public Text() { }
        public string Font { get; set; }

        public int Size { get; set; }

        public string Color { get; set; }
        //public void setFont(string font)
        //{
        //    Font = font;
        //    TextEditor.CreateSnapshot();
        //}
        //public void setSize(int size)
        //{
        //    Size = size;
        //    TextEditor.CreateSnapshot();
        //}

        public object Clone()
        {
            return new Text() { Font = this.Font, Size = this.Size, Color = this.Color };
        }

        public override string ToString()
        {
            return String.Format("Font:{0}; Size:{1}; Color: {2}", Font, Size, Color);
        }
    }
}