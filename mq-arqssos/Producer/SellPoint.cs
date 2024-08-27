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
            string[] products = { "Coca-Cola", "Água sem gás", "Arroz", "Feijão", "Pão Francês" };

            Random random = new Random();

            int i = 0;
            while (i < 10)
            {
                var productName = products[random.Next(0, products.Length)];
                var quantity = random.Next(-10, 11);

                Console.WriteLine($"[Pv {_sellPointid} Msg {i}] -> {productName} {quantity}");
                _queue.EnqueueMessage($"Pv {_sellPointid} Msg {i}", productName, quantity);

                Thread.Sleep(0);
                i++;
            }
        }
    }
}
