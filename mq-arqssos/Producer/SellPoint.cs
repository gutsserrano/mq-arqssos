using mq_arqssos.Consumer;

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
            while (i < 30)
            {
                var productName = products[random.Next(0, products.Length)];
                var quantity = random.Next(-10, 11);

                _queue.EnqueueMessage($"Pid {_sellPointid} Msg {i}", productName, quantity);

                Console.WriteLine($"---> [Pid {_sellPointid} Msg {i}] enviou: {productName} Quantidade: {quantity}");
                Thread.Sleep(1000);
                i++;
            }
        }
    }
}
