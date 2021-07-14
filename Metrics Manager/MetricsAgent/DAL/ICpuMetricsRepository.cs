using Dapper;
using Metrics_Manager.Models;
using MetricsAgent.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetrics>
    {
        void Create(CPUMetrics cPUMetrics);
    }


    public class CpuMetricsRepository : IRepository<CpuMetrics>
    {
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

        

        public void Create(CpuMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
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
                connection.Execute("DELETE FROM cpumetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }

        public IList<CpuMetrics> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                
                return connection.Query<CpuMetrics>("SELECT Id, Time, Value FROM cpumetrics").ToList();
            }
        }


        public CpuMetrics GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<CpuMetrics>("SELECT Id, Time, Value FROM cpumetrics WHERE id=@id",
                    new { id = id });
            }

        }

        public void Update(CpuMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id",
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
