using System;

namespace Patterns
{
    public class OrderClientDemo
    {
        public static void MainOrderClientDemo(string[] args)
        {
            Order order = new Order();
            order.Send();
            order.Pay();
            order.AddProduct("Product 1");
            order.AddProduct("Product 2");
            order.Send();
            Console.WriteLine(order.State);
            order.Pay();
            Console.WriteLine(order.State);
            order.Cancel();

        }
    }
}