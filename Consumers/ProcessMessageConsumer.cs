using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MTNMessages.Consumers
{
    public class ProcessMessageConsumer : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }
    }
}