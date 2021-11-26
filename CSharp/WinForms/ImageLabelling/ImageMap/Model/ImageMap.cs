using System;
using System.Collections.Generic;
using System.Text;

namespace ImageMap.Model
{
    public class ImageMap
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<ImageObject> ImageObjects { get; set; }
        public ImageMap() {
            ImageObjects = new List<ImageObject>();
        }
    }
}
