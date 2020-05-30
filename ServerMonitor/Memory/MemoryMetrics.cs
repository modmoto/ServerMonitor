namespace ServerMonitor.Memory
{
    public class MemoryMetrics
    {
        public double Free { get; set; }
        public double Total { get; set; }
        public double Used { get; set; }
        public double UsedPercentage => Used / Total;
    }
}