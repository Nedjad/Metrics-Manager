using Metrics_Manager.Request;
using Metrics_Manager.Responce;
using Metrics_Manager.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Metrics_Manager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
            private readonly HttpClient _httpClient;
            private readonly ILogger _logger;

            public MetricsAgentClient(HttpClient httpClient, ILogger logger)
            {
                _httpClient = httpClient;
                _logger = logger;
            }

            public async Task<AllCpuMetricsResponses> GetAllCpuMetrics(CpuMetricsApiRequest request)
            {
                var fromParameter = request.FromTime;
                var toParameter = request.ToTime;
                var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                    $"{request.AgentUrl}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");
                try
                {
                    HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<AllCpuMetricsResponses>(responseStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return null;
            }

            public async Task<AllDotNetMetricsResponses> GetAllDotNetMetrics(DotNetMetricsApiRequest request)
            {
                var fromParameter = request.FromTime;
                var toParameter = request.ToTime;
                var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                    $"{request.AgentUrl}/api/metrics/dotnet/errors-count/from/{fromParameter}/to/{toParameter}");
                try
                {
                    HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<AllDotNetMetricsResponses>(responseStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return null;
            }

            public async Task<AllHddMetricsResponses> GetAllHardDriveMetrics(HddMetricsApiRequest request)
            {
                var fromParameter = request.FromTime;
                var toParameter = request.ToTime;
                var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                    $"{request.AgentUrl}/api/metrics/hdd/left/from/{fromParameter}/to/{toParameter}");
                try
                {
                    HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<AllHddMetricsResponses>(responseStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return null;
            }


            public async Task<AllNetworkMetricsResponses> GetAllNetworkMetrics(NetworkMetricsApiRequest request)
            {
                var fromParameter = request.FromTime;
                var toParameter = request.ToTime;
                var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                    $"{request.AgentUrl}/api/metrics/network/from/{fromParameter}/to/{toParameter}");
                try
                {
                    HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<AllNetworkMetricsResponses>(responseStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return null;
            }

            public async Task<AllRamMetricsResponses> GetAllRamMetrics(RamMetricsApiRequest request)
            {
                var fromParameter = request.FromTime.ToUnixTimeSeconds();
                var toParameter = request.ToTime.ToUnixTimeSeconds();
                var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                    $"{request.AgentUrl}/api/metrics/ram/available/from/{fromParameter}/to/{toParameter}");
                try
                {
                    HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<AllRamMetricsResponses>(responseStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return null;
            }

        Task<AllCpuMetricsResponses> IMetricsAgentClient.GetAllCpuMetrics(CpuMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

