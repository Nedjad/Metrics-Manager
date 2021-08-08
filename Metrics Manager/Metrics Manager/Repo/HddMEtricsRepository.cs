using Metrics_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.Repo
{
    public class HddMEtricsRepository
    {
        private ConnectionManager _connection = new ConnectionManager();

        public IList<HddMetric> GetMetricsFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.GetOpenedConnection();

            return connection.Query<HddMetric>(
                "SELECT * FROM hardDrivemetrics WHERE AgentId = @agentId AND Time BETWEEN @FromTime AND @toTime",
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

            return connection.ExecuteScalar<DateTimeOffset>("SELECT MAX(Time) FROM HardDrivemetrics WHERE AgentId = @AgentId",
                new
                {
                    AgentId = agentId
                });
        }

        public List<HddMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.GetOpenedConnection();

            return connection.Query<HddMetric>(
                "SELECT * FROM hardDrivemetrics WHERE Time BETWEEN @FromTime AND @toTime",
                new
                {
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public void Create(HddMetric item)
        {
            using var connection = _connection.GetOpenedConnection();

            connection.Execute("INSERT INTO hardDrivemetrics(AgentId, Value, Time) VALUES(@agentId, @Value, @Time)",
                new
                {
                    agentId = item.Id,
                    Value = item.Value,
                    Time = item.Time.ToUnixTimeSeconds()
                });
        }
    }
}
