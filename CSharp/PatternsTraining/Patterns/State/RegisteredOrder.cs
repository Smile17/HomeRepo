namespace Patterns
{
    public class RegisteredOrder : IOrderState
    {
        public Order Order { get; set; }

        public void Pay()
        {
            Order.State = new ShippedOrder(Order);
            Order.State.Pay();
        }

        public void Send()
        {
            Order.State = new ShippedOrder(Order);
        }

        public void AddProduct(string productName)
        {
            Order.Products.Add(productName);
        }

        public void Cancel() => Order.State = new CancelledOrder(Order);

        public RegisteredOrder(Order order) => Order = order;
    }

    }