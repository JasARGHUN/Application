using System.Diagnostics;

namespace Application.Infrastructure
{
    public class UptimeService
    {
        private Stopwatch timer;
        public UptimeService()
        {
            timer = Stopwatch.StartNew();
        }
        public long Uptime =>
            timer.ElapsedMilliseconds;
    }
}
