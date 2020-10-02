using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RpiRelayApp.BusinessLogic
{
    public class Worker : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRelayController _relayController;

        public Worker(ILogger<Worker> logger, IRelayController relayController)
        {
            _logger = logger;
            _relayController = relayController;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running next cycle at: {time}", DateTimeOffset.Now);

                    _relayController.InitializeBoard();
                    await _relayController.PerformGpioCheck();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error occurred in Backgroundworker, retrying next cycle");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
