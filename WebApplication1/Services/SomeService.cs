namespace WebApplication1.Services
{
    public class SomeService
    {
        private readonly JobTracker _jobTracker;
        public SomeService(JobTracker jobTracker)
        {
            _jobTracker = jobTracker;
        }
    }
}