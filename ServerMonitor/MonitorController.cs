using Microsoft.AspNetCore.Mvc;
using ServerMonitor.Ports;

namespace ServerMonitor
{
    [ApiController]
    [Route("api/monitor")]
    public class MonitorController : ControllerBase
    {
        private readonly IMemoryMetricsClient _metricsClientLinux;

        public MonitorController(IMemoryMetricsClient metricsClientLinux)
        {
            _metricsClientLinux = metricsClientLinux;
        }

        [HttpGet("memory")]
        public IActionResult GetData()
        {
            var memoryMetrics = _metricsClientLinux.GetMetrics();
            return Ok(memoryMetrics);
        }
    }
}