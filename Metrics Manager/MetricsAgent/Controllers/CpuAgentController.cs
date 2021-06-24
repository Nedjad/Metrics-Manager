using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuAgentController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsAgent(int agentId,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            return Ok();
        }
    }
}
