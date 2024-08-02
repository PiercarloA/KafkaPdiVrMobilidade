using Confluent.Kafka;

class Program
{
    public static void Main(string[] args)
    {

        var conf = new ConsumerConfig
        {
            GroupId = "teste-consumer-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var c = new ConsumerBuilder<string, string>(conf).Build())
        {
            c.Subscribe("meutopico");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        var resultadoDoConsumo = c.Consume(cts.Token);
                        Console.WriteLine($"Consumiu a mensagem '{resultadoDoConsumo.Value}' no tópico [[partição]] @offset: '{resultadoDoConsumo.TopicPartitionOffset}' Chave: {resultadoDoConsumo.Key}.");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Ocorreu um erro ao consumir mensagem: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                c.Close();
            }
        }

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
