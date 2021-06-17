using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuAgentControllerUnitTests
    {
        private CpuAgentController Controller;

        public CpuAgentControllerUnitTests()
        {
            Controller = new CpuAgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_returnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            var result = Controller.GetMetricsAgent(agentId, fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class DotNetAgentControllerUnitTests
    {
        private CpuAgentController Controller;

        public DotNetAgentControllerUnitTests()
        {
            Controller = new CpuAgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_returnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            var result = Controller.GetMetricsAgent(agentId, fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class HddAgentControllerUnitTests
    {
        private CpuAgentController Controller;

        public HddAgentControllerUnitTests()
        {
            Controller = new CpuAgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_returnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            var result = Controller.GetMetricsAgent(agentId, fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class NetworkAgentControllerUnitTests
    {
        private CpuAgentController Controller;

        public NetworkAgentControllerUnitTests()
        {
            Controller = new CpuAgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_returnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            var result = Controller.GetMetricsAgent(agentId, fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class RamAgentControllerUnitTests
    {
        private CpuAgentController Controller;

        public RamAgentControllerUnitTests()
        {
            Controller = new CpuAgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_returnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            var result = Controller.GetMetricsAgent(agentId, fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
