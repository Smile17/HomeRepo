namespace Patterns
{
    public class CarDirector
    {
        public ICarBuilder Builder { get; set; }
        public CarDirector(ICarBuilder builder)
        {
            Builder = builder;
        }
        public void ConstructCar()
        {
            Builder.Reset();
            Builder.Assemble();
            Builder.AddDoor();
            Builder.Tune();
        }
    }
}