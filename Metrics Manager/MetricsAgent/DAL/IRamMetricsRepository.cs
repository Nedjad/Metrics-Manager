using Dapper;
using MetricsAgent.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IRamMetricsRepository : IRepository<RamMetrics>
    {
    }

    public class RamMetricsRepository : IRepository<RamMetrics>
    {
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

        public void Create(RamMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    new
                    {

                        value = item.Value,

                        time = item.Time.TotalSeconds
                    });
            }

        }

        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM rammetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }

        public IList<RamMetrics> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {

                return connection.Query<RamMetrics>("SELECT Id, Time, Value FROM rammetrics").ToList();
            }
        }

        public RamMetrics GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<RamMetrics>("SELECT Id, Time, Value FROM rammetrics WHERE id=@id",
                    new { id = id });
            }
        }

        public void Update(RamMetrics item)
        {

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }
    }
}
