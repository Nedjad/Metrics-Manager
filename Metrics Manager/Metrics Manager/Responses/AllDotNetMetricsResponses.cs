using Metrics_Manager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.Responce
{
    public class AllDotNetMetricsResponses
    {
        public List<DotNetMetricDto> Metrics { get; set; }

        public AllDotNetMetricsResponses()
        {
            Metrics = new List<DotNetMetricDto>();
        }
    }
}
