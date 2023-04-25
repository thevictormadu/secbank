namespace SecBank.BgService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IService _service;

        public Worker(ILogger<Worker> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service started...");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has been stopped...");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var result = _service.PostTransaction();
                if (result)
                {
                    _logger.LogInformation("Transaction Processed \n");
                }
                else
                {
                    _logger.LogError("Transaction Not Processed\n \n \n");
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}