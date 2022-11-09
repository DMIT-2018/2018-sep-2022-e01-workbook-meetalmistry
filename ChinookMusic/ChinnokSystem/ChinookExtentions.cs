

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional Namespaces
using ChinnokSystem.DAL;
using ChinnokSystem.BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace ChinnokSystem
{
    //your class needs to be public so it can be used outside of this project
    //this class also needs to be static
   public static class ChinookExtentions
    {
        //method name can be anything, it must match the builder.Services.xxxxx(options =>...statement in your program.cs


        //the first perameter is the class  that you are attemptimng to extend

        //the second perameter is the options value in your call statement

        //it is receiving the connection string fro your application
        public static void ChinookSystemBackendDependencies(
            this IServiceCollection services,
            //taking from program.cs
            Action<DbContextOptionsBuilder> options
            )
        {
            //register the DbContext class with the services collection
            services.AddDbContext<ChinookContext>(options);

            //add any services that yiu create in the class library using.AddTrainsient<serviceclassname>(....);

            services.AddTransient<TrackServices>((serviceProvider) => 
            {
                //retrive the registered Dbcontext doen with Add context
                var context = serviceProvider.GetRequiredService<ChinookContext>();
                return new TrackServices(context);
            
            });

            services.AddTransient<PlaylistTrackServices>((serviceProvider) =>
            {
                //retrive the registered Dbcontext doen with Add context
                var context = serviceProvider.GetRequiredService<ChinookContext>();
                return new PlaylistTrackServices(context);

            });
        }
    }
}
