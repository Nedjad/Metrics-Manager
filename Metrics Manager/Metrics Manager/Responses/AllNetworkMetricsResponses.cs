using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.Responses
{
    public class AllNetworkMetricsResponses
    {
        public List<NetworkMetricDto> Metrics { get; set; }

        public AllNetworkMetricsResponses()
        {
            Metrics = new List<NetworkMetricDto>();
        }
    }
}
