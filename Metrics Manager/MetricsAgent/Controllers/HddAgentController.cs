using MetricsAgent.DAL;
using MetricsAgent.Model;
using MetricsAgent.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HddAgentController : ControllerBase
    {
        private IHddMetricsRepository repository;
        private readonly ILogger<HddAgentController> _logger;

        public HddAgentController(ILogger<HddAgentController> logger, IHddMetricsRepository repo)
        {
            this.repository = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetrics request)
        {
            repository.Create(new HddMetrics
            {
                Time = request.Time,
                Value = request.Value

            });
            return Ok();

        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = repository.GetAll();

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpGet("left")]
        public IActionResult GetFreeDiskSpace()
        {
            _logger.LogInformation("HddLog Left");

            return Ok();
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeDiskForPeriod([FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("HddLog");

            return Ok();
        }
    }
}
