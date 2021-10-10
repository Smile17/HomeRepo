using System;

namespace Patterns
{
    public class NewOrder : IOrderState
    {
        public Order Order { get; set;}
        public NewOrder(Order order) => Order = order;

        public void Cancel() => Order.State = new CancelledOrder(Order);

        public void Pay()
        {
            if (Order.Products.Count == 0)
            {
                Console.WriteLine("Order can not be paid since it is empty");
            }
            else
            {
                Order.State = new RegisteredOrder(Order);
                Order.State.Pay();
            }
        }

        public void Send()
        {
            if (Order.Products.Count == 0)
            {
                Console.WriteLine("Order can not be sent since it is empty");
            }
            else {
                Order.State = new RegisteredOrder(Order);
                Order.State.Send();
            }
        }

        public void AddProduct(string productName)
        {
            Order.Products.Add(productName);
        }
    }
}