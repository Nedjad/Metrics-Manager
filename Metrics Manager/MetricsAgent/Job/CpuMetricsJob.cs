using System;
using MetricsAgent.DAL;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using MetricsAgent.Model;

namespace MetricsAgent.Job
{
    public class CpuMetricsJob : IJob
    {
        private ICpuMetricsRepository _repository;

        // счетчик для метрики CPU
        private PerformanceCounter _cpuCounter;


        public CpuMetricsJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow;

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new CPUMetrics() { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
