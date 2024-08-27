using mq_arqssos.Queue;

namespace mq_arqssos.Consumer
{
    public class Stock
    {
        public List<Product> products = new List<Product>();
        private MessageQueue _queue;

        public static Stock instance { get; private set; }

        private Stock(MessageQueue queue)
        {
            _queue = queue;
        }

        public async void Consume()
        {
            while (true)
            {
                if (_queue.TryDequeue(out Tuple<string, string, int> message))
                {
                    if(message.Item1 == null && message.Item2 == null && message.Item3 == 0)
                    {
                        break;
                    }

                    await HandleProduct(message.Item1, message.Item2, message.Item3);
                }
            }
        }

        public Task HandleProduct(string sellId, string productName, int quantity)
        {
            var msg = "";

            var product = products.Find(p => p.Name == productName);

            if (products.Find(p => p.Name == productName) == null)
            {
                if (quantity >= 0)
                {
                    products.Add(new Product
                    {
                        Name = productName,
                        Quantity = quantity
                    });

                    msg = $"[{sellId}] Cadastro de produto: {productName} +{quantity} | Total: {quantity}";
                }
                else
                {
                    msg = $"[{sellId}] Venda não pode ser efetuada. {productName} não cadastrado(a)";
                }
            }
            else if (quantity > 0)
            {
                AddQuantityInStock(productName, quantity);
                msg = $"[{sellId}] Reestoque: {productName} +{quantity} | Total: {product.Quantity}";
            }
            else
            {
                if (RemoveQuantityInStock(productName, -quantity))
                {
                    msg = $"[{sellId}] Venda: {productName} {quantity} | Total: {product.Quantity}";
                }
                else
                {
                    msg = $"[{sellId}] Venda não pode ser efetuada. {productName} {quantity} | Total: {product.Quantity}";
                }
            }
            Console.WriteLine(msg);

            return Task.CompletedTask;
        }

        public void AddQuantityInStock(String productName, int quantity)
        {
            products.Find(p => p.Name.Equals(productName))?.IncrementQuantity(quantity);
        }

        public bool RemoveQuantityInStock(String productName, int quantity)
        {
            return products.First(p => p.Name.Equals(productName)).DecrementQuantity(quantity);
        }

        public static Stock GetInstance(MessageQueue queue)
        {
            if (instance == null)
            {
                instance = new Stock(queue);
            }

            return instance;
        }
    }
}
