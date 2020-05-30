namespace ServerMonitor.DiskSpace
{
    public class DispSpaceMetrics
    {
        public string DriveName { get; set; }
        public double Total { get; set; }
        public double Used { get; set; }
        public double UsedPercentage => Used / Total;
        public double Free { get; set; }
    }
}