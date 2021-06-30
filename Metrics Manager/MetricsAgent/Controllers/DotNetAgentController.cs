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
    public class DotNetAgentController : ControllerBase
    {
        private IDotNetMetricsRepository repo;
        private readonly ILogger<DotNetAgentController> _logger;

        public DotNetAgentController(ILogger<DotNetAgentController> logger, IDotNetMetricsRepository _repository)
        {
            this.repo = _repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetAgentController");

        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetrics request)
        {
            repo.Create(new DotNetMetrics
            {
                Time = request.Time,
                Value = request.Value

            });
            return Ok();

        }
        
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = repo.GetAll();

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCount([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("DotNetLog");
            return Ok();
        }
    }
}
