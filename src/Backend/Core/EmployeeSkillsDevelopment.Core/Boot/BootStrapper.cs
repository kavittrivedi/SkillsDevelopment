using EmployeeSkillsDevelopment.Infrastructure.Data;
using EmployeeSkillsDevelopment.Infrastructure.Interfaces;
using EmployeeSkillsDevelopment.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSkillsDevelopment.Infrastructure.Boot
{
    public static class BootStrapper
    {
        public static IServiceCollection DataBootStrapper(IServiceCollection builder)
        {
            //builder.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //builder.AddScoped<IUserRepository, UserRepository>();
            builder.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.AddScoped<IAppDbContext>(provider =>
            {
                var context = provider.GetService(typeof(AppDbContext)) as IAppDbContext;
                if (context == null)
                {
                    throw new InvalidOperationException("AppDbContext not registered.");
                }
                return context;
            });
            return builder;
        }
        public static WebApplicationBuilder ConnectionStringBootStrapper(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("mydb"));
            });
            return builder;
        }
    }
}
