using Metrics_Manager.Request;
using Metrics_Manager.Responce;
using Metrics_Manager.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics_Manager.Client
{
    interface IMetricsAgentClient
    {
        Task<AllCpuMetricsResponses> GetAllCpuMetrics(CpuMetricsApiRequest request);

        Task<AllDotNetMetricsResponses> GetAllDotNetMetrics(DotNetMetricsApiRequest request);

        Task<AllHddMetricsResponses> GetAllHardDriveMetrics(HddMetricsApiRequest request);

        Task<AllNetworkMetricsResponses> GetAllNetworkMetrics(NetworkMetricsApiRequest request);

        Task<AllRamMetricsResponses> GetAllRamMetrics(RamMetricsApiRequest request);
    }
}
