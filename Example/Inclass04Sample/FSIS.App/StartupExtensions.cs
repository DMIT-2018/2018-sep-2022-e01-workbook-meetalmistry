using FSIS.App.BLL;
using FSIS.App.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSIS.App
{
    public static class StartupExtensions
    {
        public static void AddBackendDependencies(this IServiceCollection services,
              Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<FSIS_2018Context>(options);
            services.AddTransient((serviceProvider) =>
            {
                //get the dbcontext class
                var context = serviceProvider.GetRequiredService<FSIS_2018Context>();
                return new FsisServices(context);
            });

        }
    }
}
