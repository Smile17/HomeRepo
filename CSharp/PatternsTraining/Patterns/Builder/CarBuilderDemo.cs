using System;

namespace Patterns
{
    public class CarBuilderDemo
    {
        public static void MainDemo(string[] args)
        {
            CarBuilder builder = new CarBuilder();
            CarDirector director = new CarDirector(builder);
            director.ConstructCar();
            Console.WriteLine(builder.GetResult());
        }
    }
}