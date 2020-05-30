using ServerMonitor.DiskSpace;

namespace ServerMonitor.Ports
{
    public interface IDiskSpaceMetrics
    {
        DispSpaceMetrics GetMetrics();
    }
}