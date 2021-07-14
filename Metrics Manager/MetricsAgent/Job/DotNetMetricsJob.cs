using MetricsAgent.DAL;
using MetricsAgent.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Job
{
    public class DotNetMetricsJob : IJob
    {
        private IDotNetMetricsRepository _repository;

        private PerformanceCounter _dotnetCounter;

        public DotNetMetricsJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotnetCounter = new PerformanceCounter(".NET CLR Memory", "# Total committed Bytes", "_Global_");

        }

        public Task Execute(IJobExecutionContext context)
        {

            var dotnetUssage = Convert.ToInt32(_dotnetCounter.NextValue());


            var time = TimeSpan.FromSeconds(5);



            _repository.Create(new DotNetMetrics() { Time = time, Value = dotnetUssage });

            return Task.CompletedTask;
        }
    }
}
