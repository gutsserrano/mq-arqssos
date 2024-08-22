using mq_arqssos.Consumer;

namespace mq_arqssos.Stock
{
    public class ProductStock
    {
        public List<Product> products = new List<Product>();
        private MessageQueue _queue;

        public static ProductStock instance { get; private set; }

        private ProductStock(MessageQueue queue)
        {
            _queue = queue;
        }

        public async void Consume()
        {
            while (true)
            {
                if (_queue.TryDequeue(out Tuple<string, string, int> message))
                {
                    await HandleProduct(message.Item1, message.Item2, message.Item3);
                }
            }
        }

        public Task HandleProduct(string sellId, string productName, int quantity)
        {
            var msg = "";

            if (products.Find(p => p.Name == productName) == null)
            {
                if (quantity >= 0)
                {
                    products.Add(new Product
                    {
                        Name = productName,
                        Quantity = quantity
                    });

                    msg = $"\t*** Cadastro de produto: [{sellId}] Produto: {productName} Quantidade: {quantity}";
                }
                else
                {
                    msg = $"\t*** Venda [{sellId}] não pode ser efetuada - Produto não cadastrado";
                }
            }
            else if (quantity > 0)
            {
                AddQuantityInStock(productName, quantity);
                msg = $"\t*** Reestoque: [{sellId}] Produto: {productName} Quantidade: {quantity}";
            }
            else
            {
                if (RemoveQuantityInStock(productName, -quantity))
                {
                    msg = $"\t*** Venda: [{sellId}] Produto: {productName} Quantidade: {-quantity}";
                }
                else
                {
                    msg = $"\t*** Venda [{sellId}] não pode ser efetuada - Falta de estoque";
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

        public static ProductStock GetInstance(MessageQueue queue)
        {
            if (instance == null)
            {
                instance = new ProductStock(queue);
            }

            return instance;
        }
    }
}
