namespace WebApplication1.Services
{
    public class JobTracker
    {
        private readonly IWorker _worker;

        public JobTracker(IWorker worker)
        {
            _worker = worker;
        }
    }
}