using System;

namespace Patterns
{
    public class CancelledOrder : IOrderState
    {
        public Order Order { get; set; }

        public void Pay()
        {
            Console.WriteLine("An order is cancelled");
        }

        public void Send()
        {
            Console.WriteLine("An order is cancelled");
        }

        public void AddProduct(string productName)
        {
            Console.WriteLine("An order is cancelled");
        }

        public void Cancel() {
            Order.State = new NewOrder(Order);
        }

        public CancelledOrder(Order order)
        {
            Order = order;
        }
    }
}