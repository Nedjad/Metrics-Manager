﻿using Metrics_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetrics>
    {

    }

    public class CpuMetricsReposetory : IRepository<CpuMetrics>
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public void Create(CpuMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            
            cmd.CommandText = "DELETE FROM cpumetrics WHERE id=@id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<CpuMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = "SELECT * FROM cpumetrics";

            var returnList = new List<CpuMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    
                    returnList.Add(new CpuMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnList;
        }


        public CpuMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics WHERE id=@id";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                
                if (reader.Read())
                {
                    
                    return new CpuMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(1))
                    };
                }
                else
                {
                    
                    return null;
                }
            }

        }

        public void Update(CpuMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            using var cmd = new SQLiteCommand(connection);
            
            cmd.CommandText = "UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }

}