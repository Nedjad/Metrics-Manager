using Metrics_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Metrics_Manager.Repo
{
        public class CpuMetricsRepository : CpuMetricsRepository
        {
            private ConnectionManager _connection = new ConnectionManager();

            public List<CpuMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
            {
                using var connection = _connection.GetOpenedConnection();

                return connection.Query<CpuMetrics>(
                    "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }

            public IList<CpuMetrics> GetMetricsFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
            {
                using var connection = _connection.GetOpenedConnection();

                return connection.Query<CpuMetrics>(
                    "SELECT * FROM cpumetrics WHERE AgentId = @agentId AND time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        agentId = id,
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }

            public DateTimeOffset GetLastDateFromAgent(int agentId)
            {
                using var connection = _connection.GetOpenedConnection();

                return connection.ExecuteScalar<DateTimeOffset>("SELECT MAX(Time) FROM cpumetrics WHERE AgentId = @AgentId",
                    new
                    {
                        AgentId = agentId
                    });
            }

            public void Create(CpuMetrics item)
            {
                using var connection = _connection.GetOpenedConnection();

                connection.Execute("INSERT INTO cpumetrics(agentId ,value, time) VALUES(@agentId, @value, @time)",
                    new
                    {
                        agentId = item.AgentId,
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }
}
