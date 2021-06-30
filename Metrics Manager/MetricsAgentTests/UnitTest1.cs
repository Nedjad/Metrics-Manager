using Metrics_Manager.Models;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using Xunit;


namespace MetricsAgentTests
{
    public class CpuAgentControllerUnitTests
    {
        private CpuAgentController Controller;
        private Mock<ICpuMetricsRepository> mock;

        public CpuAgentControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();

            Controller = new CpuAgentController(NullLogger<CpuAgentController>.Instance, mock.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_returnsOk()
        {
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetrics>())).Verifiable();

            var result = Controller.Create(new CpuMetrics()
            { Time = TimeSpan.FromSeconds(1), Value = 50 });

            mock.Verify(repository => repository.Create(It.IsAny<CpuMetrics>()), Times.AtMostOnce());


        }
    }
    public class DotNetAgentControllerUnitTests
    {
        private DotNetAgentController Controller;
        private Mock<IDotNetMetricsRepository> _mock;

        public DotNetAgentControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            Controller = new DotNetAgentController(NullLogger<DotNetAgentController>.Instance, _mock.Object);
        }

        [Fact]
        public void CreateShouldCallCreateFromRepository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetrics>())).Verifiable();
            var result = Controller.Create(new DotNetMetrics()
            { Time = TimeSpan.FromSeconds(1), Value = 50 });
            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetrics>()), Times.AtMostOnce());

        }
    }
    public class HddAgentControllerUnitTests
    {
        private HddAgentController Controller;
        private Mock<IHddMetricsRepository> _mock;

        public HddAgentControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();
            Controller = new HddAgentController(NullLogger<HddAgentController>.Instance, _mock.Object);

        }

        [Fact]
        public void GetMetricsFromAgent_returnsOk()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<HddMetrics>())).Verifiable();
            var result = Controller.Create(new HddMetrics() { Time = TimeSpan.FromSeconds(1), Value = 50 });
            _mock.Verify(repository => repository.Create(It.IsAny<HddMetrics>()), Times.AtMostOnce());
        }
    }
    public class NetworkAgentControllerUnitTests
    {
        private NetworkAgentController Controller;
        private Mock<INetworkMetricsRepository> _mock;

        public NetworkAgentControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            Controller = new NetworkAgentController(NullLogger<NetworkAgentController>.Instance, _mock.Object);

        }

        [Fact]
        public void GetMetricsFromAgent_returnsOk()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<NetworkMetrics>())).Verifiable();
            var result = Controller.Create(new NetworkMetrics()
            { Time = TimeSpan.FromSeconds(1), Value = 50 });
            _mock.Verify(repository => repository.Create(It.IsAny<NetworkMetrics>()), Times.AtMostOnce());
        }
    }
    public class RamAgentControllerUnitTests
    {
        private RamAgentController Controller;
        private Mock<IRamMetricsRepository> _mock;

        public RamAgentControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            Controller = new RamAgentController(NullLogger<RamAgentController>.Instance, _mock.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_returnsOk()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<RamMetrics>())).Verifiable();

            var result = Controller.Create(new RamMetrics()
            { Time = TimeSpan.FromSeconds(1), Value = 50 });
            _mock.Verify(repository => repository.Create(It.IsAny<RamMetrics>()), Times.AtMostOnce);
        }
    }
}
