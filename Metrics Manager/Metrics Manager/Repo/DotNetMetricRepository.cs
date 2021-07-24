using Metrics_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.Repo
{
    public class DotNetMetricRepository
    {
        private ConnectionManager _connection = new ConnectionManager();

        public List<DotNetMetriccs> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.GetOpenedConnection();

            return connection.Query<DotNetMetriccs>(
                "SELECT * FROM dotnetmetrics WHERE Time BETWEEN @FromTime AND @ToTime",
                new
                {
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public void Create(DotNetMetriccs item)
        {
            using var connection = _connection.GetOpenedConnection();

            connection.Execute("INSERT INTO dotnetmetrics(AgentId, Value, Time) VALUES(@agentId, @value, @time)",
                new
                {
                    agentId = item.AgentId,
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<DotNetMetriccs> GetMetricsFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.GetOpenedConnection();

            return connection.Query<DotNetMetriccs>(
                "SELECT * FROM dotnetmetrics WHERE AgentId = @agentId AND Time BETWEEN @FromTime AND @ToTime",
                new
                {
                    agentId = id,
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public DateTimeOffset GetLastDateFromAgent(int agentId)
        {
            using var connection = _connection.GetOpenedConnection();

            return connection.ExecuteScalar<DateTimeOffset>("SELECT MAX(Time) FROM dotnetmetrics WHERE AgentId = @AgentId",
                new
                {
                    AgentId = agentId
                });
        }
    }
}
