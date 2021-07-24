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
    public class DotNetMetricJob
    {
        private readonly DotNetMetricRepository _repository;
        private readonly AgentsRepository _agent;
        private readonly ILogger<DotNetMetricJob> _logger;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public DotNetMetricJob(
            DotNetMetricRepository repository,
            AgentsRepository agent,
            ILogger<DotNetMetricJob> logger,
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
                await RetrieveMetricsFromAgent(agent);
            }
        }

        private async Task RetrieveMetricsFromAgent(AgentInfo agent)
        {
            try
            {
                DateTimeOffset lastDate = _repository.GetLastDateFromAgent(agent.AgentId);

                var response = await _client.GetAllDotNetMetrics(new DotNetMetricsApiRequest
                {
                    AgentUrl = agent.AgentUrl,
                    FromTime = lastDate,
                    ToTime = DateTimeOffset.Now
                });

                if (response == null) return;

                foreach (var dotNetMetric in response.Metrics.Select(metric => _mapper.Map<DotNetMetriccs>(metric)))
                {
                    dotNetMetric.AgentId = agent.AgentId;
                    _repository.Create(dotNetMetric);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
