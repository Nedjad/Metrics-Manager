using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.Responses
{
    public class AllRamMetricsResponses
    {
        public List<RamMetricDto> Metrics { get; set; }

        public AllRamMetricsResponses()
        {
            Metrics = new List<RamMetricDto>();
        }
    }
}
