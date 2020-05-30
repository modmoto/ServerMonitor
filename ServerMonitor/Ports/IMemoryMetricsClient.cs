using ServerMonitor.Memory;

namespace ServerMonitor.Ports
{
    public interface IMemoryMetricsClient
    {
        MemoryMetrics GetMetrics();
    }
}