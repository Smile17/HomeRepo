using System;
using System.Collections.Generic;

namespace Patterns
{
    public class VisitShapeDemo
    {
        public static void MainDemo(string[] args)
        {
            List<IShape> list = new List<IShape>() { new Circle() { X = 5, Y=4 }, new Rectangle { X=3, Y=-3} };
            MoveVisitor move = new MoveVisitor() { X = 3, Y = 12 };
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Accept(move);
            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }
    }
}