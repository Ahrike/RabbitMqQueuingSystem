using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using USSDTELCOCharge.Data.Services;

namespace USSDTELCOCharge.Service
{
    public class USSDTelcoProcessor : BackgroundService
    {
        private readonly ILogger<USSDTelcoProcessor> _logger;

        private readonly IUssdTelCoChargeProcessor _processor;

        public USSDTelcoProcessor(ILogger<USSDTelcoProcessor> logger, IUssdTelCoChargeProcessor processor)
        {
            _logger = logger;

            _processor = processor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

               await Task.Delay(1000, stoppingToken);

                await _processor.UssdProcessor();
            }
        }

       
    }




}
