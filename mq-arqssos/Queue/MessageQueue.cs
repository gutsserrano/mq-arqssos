using System.Collections.Concurrent;

namespace mq_arqssos.Queue
{
    public class MessageQueue
    {
        private ConcurrentQueue<Tuple<string, string, int>> messageQueue = new ConcurrentQueue<Tuple<string, string, int>>();

        public void EnqueueMessage(string sellId, string productName, int quantity)
        {
            messageQueue.Enqueue(new Tuple<string, string, int>(sellId, productName, quantity));
        }

        public bool TryDequeue(out Tuple<string, string, int> message)
        {
            return messageQueue.TryDequeue(out message);
        }
    }
}
