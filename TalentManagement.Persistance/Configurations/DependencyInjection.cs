using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Persistance.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
           // services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllJobsHandler).GetTypeInfo().Assembly));
            return services;
        }
    }
}
