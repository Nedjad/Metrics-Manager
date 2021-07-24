using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.Responses
{
    public class AllHddMetricsResponses
    {
        public List<HardDriveMetricDto> Metrics { get; set; }

        public AllHddMetricsResponses()
        {
            Metrics = new List<HardDriveMetricDto>();
        }
    }
}
