
using EmployeeSkillsDevelopment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeSkillsDevelopment.DBMigrationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // Fetch connection string from configuration
                    var configuration = context.Configuration;
                    var connectionString = configuration.GetConnectionString("mydb");

                    // Configure DbContext
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString));
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    // Apply migrations
                    dbContext.Database.Migrate();

                    Console.WriteLine("Migrations applied successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");

                }

            }
        }
    }
}
