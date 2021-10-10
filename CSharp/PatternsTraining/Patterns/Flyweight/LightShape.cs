namespace Patterns
{
    public class LightShape : ILightShape
    {
        public Texture Texture { get; set; }
        public TextureFactory Factory { get; set; } = new TextureFactory();
        public LightShape(string texture)
        {
            Texture = Factory.GetTexture(texture);
        }

    }
}