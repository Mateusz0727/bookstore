using Bookshop.App.Jobs;
using Bookshop.Helpers;
using Hangfire;
using Hangfire.Common;

namespace Bookshop.App.Extensions.Background
{
    public class Hangfire :IJobScheduler
    {
        protected IServiceProvider ServiceProvider { get; }
        public Hangfire(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider; 
        }
        public void Schedule()
        {
            var menager = new RecurringJobManager();
            using (var scope = ServiceProvider.CreateScope())
            {
                var interfaceType = typeof(IScheduledJob);
                var collection = scope.ServiceProvider.GetService<IServiceCollection>();
                var descriptors = collection.Where(x => x.ServiceType.Name == interfaceType.Name).ToList();
                descriptors.ForEach(x =>
                {
                    var type = x.ImplementationType;
                
                    var method = type.GetMethod("Execute",new Type[] {typeof(CancellationToken)});
                    var job = new Job(type, method,CancellationToken.None);
                    menager.AddOrUpdate(type.FullName,job, "*/1 * * * *", new RecurringJobOptions());
                  
                });
            }
        }

    }
}
