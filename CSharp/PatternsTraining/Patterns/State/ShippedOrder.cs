using System;

namespace Patterns
{
    public class ShippedOrder : IOrderState
    {
        public Order Order { get; set; }
        public ShippedOrder(Order order) => Order = order;
        public void Pay()
        {
            Order.State = new InvoicedOrder(Order);
        }

        public void Send()
        {
            Console.WriteLine("An order is already sent");
        }
        public void AddProduct(string productName)
        {
            Order.Products.Add(productName);
        }

        public void Cancel() => Order.State = new CancelledOrder(Order);

        
    }
}