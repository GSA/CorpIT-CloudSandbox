using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using api.Models;
using System;

namespace api
{
    public class Startup
    {       
        public void ConfigureServices(IServiceCollection services)
        {
           Console.WriteLine("List of environment variables:");

           var enumerator = Environment.GetEnvironmentVariables().GetEnumerator();

           while (enumerator.MoveNext())
           {
               Console.WriteLine($"{enumerator.Key,5}:{enumerator.Value,100}");
           }

            Console.WriteLine("DATABASE_URL:");

           Console.WriteLine(Environment.GetEnvironmentVariable("DATABASE_URL"));


			/*            services.AddDbContext<AccessRequestContext>(opt => opt.UseInMemoryDatabase("AccessRequestList"));*/
			//services.AddEntityFrameworkNpgsql().AddDbContext<AccessRequestContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL")));


            var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
            //var connectionString = "Username=postgres;Password=abcd1234;Host=localhost;Port=5432;Database=EARS;Pooling=true;";
			services.AddDbContext<AccessRequestContext>(
				opts => opts.UseNpgsql(connectionString)
			);


			services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
