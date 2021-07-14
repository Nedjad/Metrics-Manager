using MetricsAgent.DAL;
using MetricsAgent.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Job
{
    public class NetworkJob : IJob
    {
        private INetworkMetricsRepository _repository;

        // счетчик для метрики CPU
        private PerformanceCounter _networkCounter;

        public NetworkJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", "Intel[R] Dual Band Wireless-AC 8260");
        }

        public Task Execute(IJobExecutionContext context)
        {

            var networkUssage = Convert.ToInt32(_networkCounter.NextValue());


            var time = TimeSpan.FromSeconds(5);



            _repository.Create(new NetworkMetrics() { Time = time, Value = networkUssage });

            return Task.CompletedTask;
        }
    }
}
