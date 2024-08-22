using mq_arqssos.Consumer;

namespace mq_arqssos.Stock
{
    public class ProductStock
    {
        public List<Product> products = new List<Product>();
        public static ProductStock instance { get; private set; }

        private ProductStock()
        { 
        }

        public void Consume()
        {
            while (true)
            {
                //MessageQueue.MessageEvent.WaitOne(); // Espera por uma nova mensagem

                if (MessageQueue.TryDequeue(out Tuple<string, int> message))
                {
                    HandleProduct(message.Item1, message.Item2);
                    var product = products.Find(p => p.Name.Equals($"{message.Item1}"));

                    if (product != null)
                    {
                        Console.WriteLine($"Product: {product.Name} - quantity {product.Quantity}");
                    }
                }

                //Thread.Sleep(100);
            }
        }

        public void HandleProduct(string productName, int quantity)
        {
            if (products.Find(p => p.Name == productName) == null)
            {
                products.Add(new Product(productName, quantity));
            }
            else if (quantity > 0)
            {
                AddQuantityInStock(productName, quantity);
            }
            else
            {
                RemoveQuantityInStock(productName, -quantity);
            }
        }

        public void AddQuantityInStock(String productName, int quantity)
        {
            products.Find(p => p.Name.Equals(productName))?.IncrementQuantity(quantity);
        }

        public void RemoveQuantityInStock(String productName, int quantity)
        {
            products.Find(p => p.Name.Equals(productName))?.DecrementQuantity(quantity);

            Tuple<String, int> tuple = new Tuple<String, int>(productName, quantity);
        }

        public static ProductStock GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductStock();
            }

            return instance;
        }
    }
}
