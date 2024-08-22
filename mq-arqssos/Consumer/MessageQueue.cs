using System.Collections.Concurrent;

namespace mq_arqssos.Consumer
{
    public static class MessageQueue
    {
        private static ConcurrentQueue<Tuple<string, int>> messageQueue = new ConcurrentQueue<Tuple<string, int>>();
        public static AutoResetEvent MessageEvent = new AutoResetEvent(false);

        public static void EnqueueMessage(string productName, int quantity)
        {
            messageQueue.Enqueue(new Tuple<string, int>(productName, quantity));
        }

        public static bool TryDequeue(out Tuple<string, int> message)
        {
            return messageQueue.TryDequeue(out message);
        }
    }
}
