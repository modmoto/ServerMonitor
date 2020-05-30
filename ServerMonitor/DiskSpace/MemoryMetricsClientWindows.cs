using System.Diagnostics;
using ServerMonitor.Ports;

namespace ServerMonitor.DiskSpace
{
    public class DiskSpaceClientWindows : IDiskSpaceMetrics
    {
        public DispSpaceMetrics GetMetrics()
        {
            var info = new ProcessStartInfo();
            info.FileName = "wmic";
            info.Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value";
            info.RedirectStandardOutput = true;

            using var process = Process.Start(info);
            var output = process.StandardOutput.ReadToEnd();

            return null;
        }
    }
}