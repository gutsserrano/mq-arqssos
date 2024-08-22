using mq_arqssos.Consumer;
using mq_arqssos.Stock;

namespace mq_arqssos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inicia a thread do produtor
            Task.Run(() => Producer.Produce(1));
            Task.Run(() => Producer.Produce(2));
            Task.Run(() => Producer.Produce(3));

            // Inicia a thread do consumidor
            Task.Run(() => ProductStock.GetInstance().Consume());

            // Mantém o programa em execução
            Console.ReadLine();
        }
    }

    public static class Producer
    {
        public static void Produce(int thread)
        {
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
                var value = random.Next(-10, 10);
                MessageQueue.EnqueueMessage($"Item{i}", value);

                Console.WriteLine($"Produced: Item{i} - {value}");
                MessageQueue.MessageEvent.Set(); // Sinaliza que há uma nova mensagem
                //Thread.Sleep(500); // Simula algum trabalho

            }
        }
    }
}
