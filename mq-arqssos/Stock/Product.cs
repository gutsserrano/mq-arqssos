namespace mq_arqssos.Stock
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Product(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public void IncrementQuantity(int increment)
        {
            Quantity += increment;
        }

        public void DecrementQuantity(int decrement)
        {
            Quantity -= decrement;
        }
    }
}
