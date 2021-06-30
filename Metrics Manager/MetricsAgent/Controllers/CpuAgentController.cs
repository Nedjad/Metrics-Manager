using Metrics_Manager.Models;
using MetricsAgent.DAL;
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
    public class CpuAgentController : ControllerBase
    {
        private ICpuMetricsRepository repository;
        private readonly ILogger<CpuAgentController> _logger;

        public CpuAgentController(ILogger<CpuAgentController> logger, ICpuMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuAgentController");
        }
        
        [HttpPost("create")]

        public IActionResult Create([FromBody] CpuMetrics request)
        {
            repository.Create(new CpuMetrics
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

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsAgent(int agentId,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("CpuLog");

            return Ok();
        }
    }
}
