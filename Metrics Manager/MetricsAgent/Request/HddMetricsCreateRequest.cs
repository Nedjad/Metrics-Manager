using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Request
{
    public class HddMetricsCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
