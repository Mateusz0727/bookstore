using Bookshop.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
namespace Bookshop.App.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterScheduledJobs(this IServiceCollection services)
        {
            services.Scan(scan => scan
                 .FromCallingAssembly()
                 .AddClasses(filter => filter
                     .Where(type =>
                         type.Namespace.Contains("Jobs") &&
                         type.Name.EndsWith("Job") &&
                         type.GetInterfaces().Contains(typeof(IScheduledJob))
                     )
                   
                 )
                 .UsingRegistrationStrategy(RegistrationStrategy.Append)
                 .AsImplementedInterfaces()
                 .AsSelf()
                 .WithTransientLifetime()
             );

            return services;
        }
    }
}
