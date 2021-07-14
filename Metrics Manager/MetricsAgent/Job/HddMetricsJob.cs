using MetricsAgent.DAL;
using MetricsAgent.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Job
{
    public class HddMetricsJob : IJob
    {
        private IHddMetricsRepository _repository;

        private PerformanceCounter _hddCounter;

        public HddMetricsJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("LogicalDisk", "% Free Space", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var hddUsage = Convert.ToInt32(_hddCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(5);

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new HddMetrics() { Time = time, Value = hddUsage });

            return Task.CompletedTask;
        }
    }
}
