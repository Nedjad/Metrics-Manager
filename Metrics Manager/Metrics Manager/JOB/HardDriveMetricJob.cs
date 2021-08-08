using AutoMapper;
using Metrics_Manager.Client;
using Metrics_Manager.Models;
using Metrics_Manager.Repo;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.JOB
{
    public class HardDriveMetricJob
    {
        private readonly HddMEtricsRepository _repository;
        private readonly AgentsRepository _agent;
        private readonly ILogger<HardDriveMetricJob> _logger;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public HardDriveMetricJob(
            HddMEtricsRepository repository,
            AgentsRepository agent,
            ILogger<HardDriveMetricJob> logger,
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

            foreach (var agents in activeAgents)
            {
                await RetrieveMetricsFromAgent(agents);
            }
        }

        private async Task RetrieveMetricsFromAgent(AgentInfo agent)
        {
            try
            {
                DateTimeOffset lastDate = _repository.GetLastDateFromAgent(agent.AgentId);

                var response = await _client.GetAllHardDriveMetrics(new HddMetricsApiRequest
                {
                    AgentUrl = agent.AgentUrl,
                    FromTime = lastDate,
                    ToTime = DateTimeOffset.Now
                });

                if (response == null) return;

                foreach (var hddMetric in response.Metrics.Select(metric => _mapper.Map<HddMetric>(metric)))
                {
                    hddMetric.AgentId = agent.AgentId;
                    _repository.Create(hddMetric);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
