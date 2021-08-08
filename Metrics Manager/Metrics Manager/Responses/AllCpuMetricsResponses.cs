using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.Responce
{
    public class AllCpuMetricsResponses
    {
        public List<CpuMetricDto> Metrics { get; set; }

        public AllCpuMetricsResponses()
        {
            Metrics = new List<CpuMetricDto>();
        }
    }
}
