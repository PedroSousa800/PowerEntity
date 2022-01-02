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
        }

        public IConfiguration Configuration { get; }
        public static string ConnectionString { get; set; }

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

            ConnectionString = "User ID=ADMIN; Password=TestASPNET2021; Data Source=dbpowerentity_high";

            if (env.IsDevelopment())
            {
                OracleConfiguration.TnsAdmin = @"C:\Pedro\Treinamento\C#\TestarConexaoWeb\TestarConexaoWeb\DB";
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

                objConn.ConnectionString = Startup.ConnectionString;

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
