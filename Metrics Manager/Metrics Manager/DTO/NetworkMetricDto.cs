using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.DTO
{
    public class NetworkMetricDto
    {
        public int Value { get; set; }
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
