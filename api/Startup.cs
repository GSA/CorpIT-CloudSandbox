using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using api.Models;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            String connectionString = null;

            //get local connection string;

            connectionString = Environment.GetEnvironmentVariable("LOCAL_CONNECTION_STRING");

            if (connectionString == null) {
              connectionString = BuildConnectionString();
            }

            Console.WriteLine("Connection String:" + connectionString);
			
                services.AddDbContext<AccessRequestContext>(
				opts => opts.UseNpgsql(connectionString)
			);


    			services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }


		
        public String BuildConnectionString()
        {

            String connectionString;
                               
			//This section retrieves credentials from the VCAP_SERVICES
			try
			{
				string vcapServices = System.Environment.GetEnvironmentVariable("VCAP_SERVICES");

				if (vcapServices != null)
				{
					dynamic json = JsonConvert.DeserializeObject(vcapServices);
					foreach (dynamic obj in json.Children())
					{

						dynamic credentials = (((JProperty)obj).Value[0] as dynamic).credentials;
						if (credentials != null)
						{
							string host = credentials.host;
							
							string username = credentials.username;
							
							string password = credentials.password;
							
                            string port = credentials.port;
                            string db_name = credentials.db_name;

                            connectionString = "Username=" + username + ";"
                                + "Password=" + password + ";"
                                + "Host=" + host + ";"
                                + "Port=" + port + ";"
                                + "Database=" + db_name + ";Pooling=true;";


                            return connectionString;

						}

					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception in BuildConnectionString:");
				Console.WriteLine(e);
                			}
            return "No Connection String";

		}

		

		

		
	}

	
}
