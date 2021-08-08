using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager
{
    public class AgentInfo
    {
        public int AgentId { get; }
        public Uri AgentAddress { get; }
        public Uri AgentUrl { get; internal set; }
        public bool IsEnabled { get; internal set; }
    }
}
