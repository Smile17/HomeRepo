namespace Patterns
{
    public class FlyWeightDemo
    {
        public static void MainDemo(string[] args)
        {
            var circle = new LightCircle("Mirrow") { X = 5, Y = 3 };
            var rectangle = new LightRectangle("Mirrow") { X = 7, Y = 8, A = 9, B = 3 };
            var tringle = new LightTriangle("Hole") { X1 = 4, Y1 = 3 };
        }
    }
}