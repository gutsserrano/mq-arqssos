namespace mq_arqssos.Stock
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public void IncrementQuantity(int increment)
        {
            Quantity += increment;
        }

        public bool DecrementQuantity(int decrement)
        {
            if (Quantity < decrement)
            {
                return false;
            }

            Quantity -= decrement;
            return true;
        }
    }
}
