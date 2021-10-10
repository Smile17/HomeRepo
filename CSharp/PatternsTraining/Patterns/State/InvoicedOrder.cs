using System;

namespace Patterns
{
    public class InvoicedOrder : IOrderState
    {
        public Order Order { get; set; }

        public void Pay()
        {
            Console.WriteLine("An order is already paid");
        }

        public void Send()
        {
            Console.WriteLine("An order is already sent");
        }

        public void AddProduct(string productName)
        {
            Console.WriteLine("An order is already paid");
        }

        public void Cancel()
        {
            Console.WriteLine("An order is already paid. You are unable to cancel it.");
        }

        public InvoicedOrder(Order order) => Order = order;
    }
}