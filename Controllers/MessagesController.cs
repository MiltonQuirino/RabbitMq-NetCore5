using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MTNMessages.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MTNMessages.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ConnectionFactory _connectionFactory;

        public MessagesController()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        [HttpPost]
        public IActionResult PostMessage([FromBody] MessageInputModel message)
        {
            using( var connection = _connectionFactory.CreateConnection() )
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(

                      queue: "messages",
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null
                      );
                    var msg = JsonConvert.SerializeObject(message);
                    var bytesMsg = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "messages",
                        basicProperties: null,
                        body: bytesMsg
                        );
                }
            }

            return Accepted();
        }
    }

}
