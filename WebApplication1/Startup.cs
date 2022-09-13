using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var registry = new ServiceRegistry();
            registry.Injectable<IWorker>();
            registry.AddTransient<SomeService>();
            registry.AddSingleton<JobTracker>();
            
            var container = new Container(registry);

            var nestedContainer = container.GetNestedContainer();
            nestedContainer.Inject<IWorker>(new NoOpWorker(), replace: true);
            
            // SomeService depends on JobTracker who depends on IWorker
            var transitiveDependent = nestedContainer.GetService<SomeService>(); // transitiveDependent.JobTracker.Worker == null
            var directDependent = nestedContainer.GetService<JobTracker>(); // directDependent.Worker == null
            var directResolution = nestedContainer.GetService<IWorker>(); // directResolution != null
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
        }
    }
}