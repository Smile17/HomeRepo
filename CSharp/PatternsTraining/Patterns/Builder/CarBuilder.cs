namespace Patterns
{
    public class CarBuilder : ICarBuilder
    {
        public Car Car { get; set; } = new Car();
        public Car GetResult()
        {
            return Car;
        }

        public void AddDoor()
        {
            Car.Door = "Big door";
        }

        public void Assemble()
        {
            Car.Skeleton = "Car skeleton";
        }

        public void Reset()
        {
            Car = new Car();
        }

        public void Tune()
        {
            Car.Color = "Blue";
        }
    }
}