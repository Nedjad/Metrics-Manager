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
    public class RamAgentController : ControllerBase
    {
        private IRamMetricsRepository repository;
        private readonly ILogger<RamAgentController> _logger;

        public RamAgentController(ILogger<RamAgentController> logger, IRamMetricsRepository repo)
        {
            this.repository = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetrics request)
        {
            repository.Create(new RamMetrics
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

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpGet("available")]
        public IActionResult GetFreeSpaceRum()
        {
            _logger.LogInformation("First Ram Log");

            return Ok();
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeRamForPeriodOfTime([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Second Ram Log");

            return Ok();
        }
    }
}
