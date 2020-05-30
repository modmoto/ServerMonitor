using System;
using System.Diagnostics;
using ServerMonitor.Ports;

namespace ServerMonitor.Memory
{
    public class MemoryMetricsClientLinux : IMemoryMetricsClient
    {
        public MemoryMetrics GetMetrics()
        {
            var info = new ProcessStartInfo("free -m");
            info.FileName = "/bin/bash";
            info.Arguments = "-c \"free -m\"";
            info.RedirectStandardOutput = true;

            using var process = Process.Start(info);
            var output = process.StandardOutput.ReadToEnd();

            var lines = output.Split("\n");
            var memory = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var metrics = new MemoryMetrics();
            metrics.Total = double.Parse(memory[1]);
            metrics.Used = double.Parse(memory[2]);
            metrics.Free = double.Parse(memory[3]);

            return metrics;
        }
    }
}