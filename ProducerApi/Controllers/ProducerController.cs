using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace ProducerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProducerController : ControllerBase
    {
        [HttpPost]
        public async Task Enviar([FromBody] string mensagem)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            
            using (var p = new ProducerBuilder<string, string>(config).Build())
            {
                try
                {
                    await p.ProduceAsync("meutopico", new Message<string, string> { Key = $"{DateTime.Now.ToString("yyyyMMdd-HHmmss")}", Value = mensagem });
                    Console.WriteLine($"Mensagem enviada");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Error ao enviar mensagem: {e.Error.Reason}");
                }
            }
        }
    }
}
