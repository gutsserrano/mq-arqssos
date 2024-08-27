using mq_arqssos.Queue;

namespace mq_arqssos.Producer
{
    public class SellPoint
    {
        private MessageQueue _queue;
        private int _sellPointid;

        public SellPoint(MessageQueue queue, int sellPointId)
        {
            _queue = queue;
            _sellPointid = sellPointId;
        }

        public void ProduceMessage()
        {
            Random random = new Random();
            string[] products = { "Coca-Cola", "Água sem gás", "Arroz", "Feijão", "Pão Francês" };
            
            for (int i = 0; i < 10; i++)
            {
                var productName = products[random.Next(0, products.Length)];
                var quantity = random.Next(-10, 11);

                _queue.EnqueueMessage($"Pv {_sellPointid} Msg {i}", productName, quantity);

                Console.WriteLine($"[Pv {_sellPointid} Msg {i}] -> {productName} {quantity}");
            }
        }
    }
}
