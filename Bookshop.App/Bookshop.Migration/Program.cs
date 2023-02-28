using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Bookshop.Migration
{
    public class Program
    {



        static void Main(string[] args)
        {

            using (var serviceProvider = CreateServices())
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }

        }
        private static ServiceProvider CreateServices()
        {
            var projectPath = ".";
            if (Debugger.IsAttached)
            {
                projectPath = "..\\..\\Bookshop.App\\Bookshop.App\\";
            }
            var configPath = Path.Combine(projectPath, "appsettings.json");
            var configfile = new FileInfo(configPath);

            var config = new ConfigurationBuilder()
                    .SetBasePath(configfile.DirectoryName)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile("secret.json", optional: false)
                    .Build();
            return new ServiceCollection()

                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(config.GetConnectionString("DefaultConnectionString"))
                    .WithGlobalCommandTimeout(TimeSpan.FromSeconds(300))
                   .WithMigrationsIn(typeof(Program).Assembly))
                .AddLogging(lb => lb.AddFluentMigratorConsole())

                .BuildServiceProvider(false);
        }
        private static Dictionary<long, string> UpdateDatabase(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var info = new Dictionary<long, string>();
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>() as MigrationRunner;
                info = runner?.MigrationLoader
                    .LoadMigrations()
                    .ToDictionary(p => p.Key, p => p.Value.Description);
                runner?.MigrateUp();

                return info;
            }
        }




    }
    public static class Migrator
    {
        public static Dictionary<long, string> Run(IConfiguration configuration)
        {

            var serviceProvider = CreateServices();
            return UpdateDatabase(serviceProvider);

        }
        private static ServiceProvider CreateServices()
        {
            var projectPath = ".";
            if (Debugger.IsAttached)
            {
                projectPath = "..\\..\\Bookshop.App\\Bookshop.App\\";
            }
            var configPath = Path.Combine(projectPath, "appsettings.json");
            var configfile = new FileInfo(configPath);

            var config = new ConfigurationBuilder()
                    .SetBasePath(configfile.DirectoryName)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile("secret.json", optional: false)
                    .Build();
            return new ServiceCollection()

                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(config.GetConnectionString("DefaultConnectionString"))
                    .WithGlobalCommandTimeout(TimeSpan.FromSeconds(300))
                    .ScanIn(typeof(Program).Assembly).For.Migrations())


                .AddLogging(lb => lb.AddFluentMigratorConsole())

                .BuildServiceProvider(false);
        }
        private static Dictionary<long, string> UpdateDatabase(IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var info = new Dictionary<long, string>();
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>() as MigrationRunner;
                info = runner?.MigrationLoader
                    .LoadMigrations()
                    .ToDictionary(p => p.Key, p => p.Value.Description);
                runner?.MigrateUp();

                return info;
            }


        }
    }
}