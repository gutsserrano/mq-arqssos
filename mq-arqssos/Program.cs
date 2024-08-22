﻿using mq_arqssos.Consumer;
using mq_arqssos.Producer;
using mq_arqssos.Stock;

namespace mq_arqssos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var messageQueue = new MessageQueue();

            Task.Run(() => new SellPoint(messageQueue, 111).ProduceMessage());
            Task.Run(() => new SellPoint(messageQueue, 222).ProduceMessage());
            Task.Run(() => new SellPoint(messageQueue, 333).ProduceMessage());

            Task.Run(() => ProductStock.GetInstance(messageQueue).Consume());

            Console.ReadLine();
        }
    }
}
