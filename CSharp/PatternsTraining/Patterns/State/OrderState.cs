namespace Patterns
{
    public interface IOrderState
    {
        Order Order { get; set; }

        void Send();
        void Cancel();
        void Pay();
        void AddProduct(string productName);
    }
}