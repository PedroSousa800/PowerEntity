using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace PowerEntity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SetConnectionString(Configuration.GetConnectionString("DefaultConnection"));
        }

        public IConfiguration Configuration { get; }

        private static string ConnectionString;

        public static string GetConnectionString()
        {
            return ConnectionString;
        }

        private static void SetConnectionString(string value)
        {
            ConnectionString = value;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PowerEntity", Version = "v1" });
                c.ExampleFilters();
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PowerEntity v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                //OracleConfiguration.TnsAdmin = @"C:\Pedro\Treinamento\C#\TestarConexaoWeb\TestarConexaoWeb\DB";
                OracleConfiguration.TnsAdmin = @"/home/pedro/Development/ASP.NET/PowerEntity/PowerEntity/DB";
            }
            else
            {
                OracleConfiguration.TnsAdmin = @".\DB";
            }

            OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;

            OracleConfiguration.CommandTimeout = 60;


            // This part below is contigence to solve a bug in ODP (Oracle.ManagedDataAccess.Client).
            // Some times when call de the page on the first time this bug appears. 
            // The message bug is: ORA-12570: Network Session: Unexpected packet read error

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                try
                {
                    objConn.Open();

                }
                catch (Exception)
                {


                }
                finally
                {
                    objConn.Close();
                }
            }
        }
    }
}
