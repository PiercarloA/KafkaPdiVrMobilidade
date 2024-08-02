# Kafka com .NET

- Criar ambiente: docker-compose up -d

- Visualizar containers: docker container stats

- Acessar container: docker container exec -it kafka-kafka-1 bash

- Criar topico: kafka-topics --bootstrap-server localhost:9092 --topic meutopico --create --partitions 2

- Listar topicos: kafka-topics --list --bootstrap-server localhost:9092

# Testando KAFKA sem executar aplicação .NET
Com o ambiente KAFKA em execução, pode se enviar e receber mensagens somente com os comandos abaixo:

    Produtor
        docker container exec -it kafka-kafka-1 bash
        kafka-console-producer --broker-list localhost:9092 --topic meutopico

    Consumidor
        docker container exec -it kafka-kafka-1 bash
        kafka-console-consumer --bootstrap-server localhost:9092 --topic meutopico

![App Screenshot](https://raw.githubusercontent.com/PiercarloA/KafkaPdiVrMobilidade/master/Media/Teste%20Kafka%20pelo%20console.jpg?token=GHSAT0AAAAAACVULOQQSLRD6MLIZXTDSDAMZVNLJ6A)

# Testando KAFKA com aplicação .NET
Utilizando o ProducerApi e ConsumerApi, basta executar os projetos e acionar a rota do produtor com a mensagem desejada que o Consumer vai consumir a mensagem.

![App Screenshot](https://raw.githubusercontent.com/PiercarloA/KafkaPdiVrMobilidade/master/Media/Teste%20Kafka%20pela%20aplica%C3%A7%C3%A3o.gif?token=GHSAT0AAAAAACVULOQQ7K5QWWCHCDKIKVFQZVNLI3Q)
