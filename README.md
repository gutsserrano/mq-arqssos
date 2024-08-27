# **message queue - arqssos**

## **Descrição**

Projeto desenvolvido para a matéria de Sistemas Operacionais com o objetivo de implementar uma fila de mensagens. Para exemplificar, foi escolhido um caso de produtor-consumidor que simula acessos de pontos de venda a um único estoque.

## **Componentes**

- **Product**: Classe que representa um produto e sua quantidade no estoque.
- **Stock**: Classe que gerencia o estoque e processa as mensagens presentes na fila.
- **SellPoint**: Classe que simula um ponto de venda, enviando mensagens para a fila.
- **MessageQueue**: Classe que implementa uma fila de mensagens utilizando `ConcurrentQueue`, permitindo o gerenciamento seguro em um ambiente multithread.

## **Relação Produtor-Consumidor**

- **Produtor**: No contexto do projeto, a classe `SellPoint` atua como produtor. Gerando e enviando mensagens para a fila.
  
- **Consumidor**: A classe `Stock` atua como consumidor, que processando as mensagens recebidas na fila e atualizando o estoque conforme necessário, adicionando novas quantidades ou decrementando o estoque.

![Untitled (1)](https://github.com/user-attachments/assets/9b336cb9-e082-448b-aa19-f82cb2dc0e72)

