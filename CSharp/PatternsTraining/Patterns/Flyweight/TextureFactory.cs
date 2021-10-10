using System.Collections.Generic;

namespace Patterns
{
    public class TextureFactory
    {
        public static List<Texture> Cache = new List<Texture>();
        public Texture GetTexture(string texture)
        {
            for (int i = 0; i < Cache.Count; i++)
            {
                if (Cache[i].TextureValue == texture) return Cache[i];
            }
            var el = new Texture() { TextureValue = texture };
            Cache.Add(el);
            return el;
        }
    }
}