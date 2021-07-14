using MetricsAgent.DAL;
using MetricsAgent.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Job
{
    public class RamMetricsJob : IJob
    {
        private IRamMetricsRepository _repository;

        private PerformanceCounter _ramCounter;

        public RamMetricsJob(IRamMetricsRepository repository)
        {
            _repository = repository;

            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramUsage = Convert.ToInt32(_ramCounter.NextValue());

            var time = TimeSpan.FromSeconds(5);

            _repository.Create(new RamMetrics { Time = time, Value = ramUsage });

            return Task.CompletedTask;
        }
    }
}
