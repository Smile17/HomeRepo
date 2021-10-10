using System;
using System.Collections.Generic;

namespace Patterns
{
    public class Order
    {
        public IOrderState State {get; set;}
        public Order()
        {
            State = new NewOrder(this);
        }

        public List<string> Products { get; set; } = new List<string>();

        public void Send()
        {
            State.Send();
        }

        public void AddProduct(string productName)
        {
            State.AddProduct(productName);
        }

        public void Cancel()
        {
            State.Cancel();
        }

        public void Pay()
        {
            State.Pay();
        }

        public override string ToString()
        {
            return String.Join(", ", Products);
        }
    }
}