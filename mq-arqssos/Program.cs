using mq_arqssos.Consumer;
using mq_arqssos.Producer;
using mq_arqssos.Queue;

namespace mq_arqssos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var messageQueue = new MessageQueue();

            // Espera a execução de todos os pontos de venda apenas para que a saída seja mais organizada

            Console.WriteLine("Mensagens produzidas:\n");
            Task.WhenAll(
                Task.Run(() => new SellPoint(messageQueue, 1).ProduceMessage()),
                Task.Run(() => new SellPoint(messageQueue, 2).ProduceMessage()),
                Task.Run(() => new SellPoint(messageQueue, 3).ProduceMessage())
                ).Wait();

            Console.WriteLine("\nConsumindo mensagens:\n");
            Task.Run(() => Stock.GetInstance(messageQueue).Consume());


            Console.ReadLine();
        }
    }
}
