using AutoMapper;
using Metrics_Manager.Client;
using Metrics_Manager.Models;
using Metrics_Manager.Repo;
using Metrics_Manager.Request;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.JOB
{
    public class RamMetricJob
    {
        private readonly RamMetricRepository _repository;
        private readonly AgentsRepository _agent;
        private readonly ILogger<RamMetricJob> _logger;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public RamMetricJob(
            RamMetricRepository repository,
            AgentsRepository agent,
            ILogger<RamMetricJob> logger,
            IMetricsAgentClient client,
            IMapper mapper)
        {
            _repository = repository;
            _agent = agent;
            _logger = logger;
            _client = client;
            _mapper = mapper;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var activeAgents = _agent.GetRegisteredList().Where(x => x.IsEnabled);

            foreach (var agent in activeAgents)
            {
                await RetrieveMetricsFromAgents(agent);
            }
        }

        private async Task RetrieveMetricsFromAgents(AgentInfo agent)
        {
            try
            {
                DateTimeOffset lastDate = _repository.GetLastDateFromAgent(agent.AgentId);

                var response = await _client.GetAllRamMetrics(new RamMetricsApiRequest
                {
                    AgentUrl = agent.AgentUrl,
                    FromTime = lastDate,
                    ToTime = DateTimeOffset.Now
                });

                if (response == null) return;

                foreach (var ramMetric in response.Metrics.Select(metric => _mapper.Map<RamMetric>(metric)))
                {
                    ramMetric.AgentId = agent.AgentId;
                    _repository.Create(ramMetric);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
}
