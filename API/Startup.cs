using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDbGenericRepository;
using Newtonsoft.Json;
using sso_base.Models;
using sso_base.Service;

namespace SSO_BASE_NOVO {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices (IServiceCollection services) {            

            var mongoDbContext = new MongoDbContext (Configuration.GetConnectionString ("Mongo"),
                "SSO-MODELO");

            services.AddSwaggerDocument ( o => {
                o.DocumentName = "SSO-BASE";
                o.Title = "SSO-BASE";
                o.Description = "Modelo base de SSO para ser usado como template";
            });
            

            services.AddHealthChecks()
                .AddMongoDb(name: "Mongo Aplicação",
                        mongodbConnectionString : Configuration.GetConnectionString ("Mongo"));
             services.AddHealthChecks()
                .AddMongoDb(name: "Mongo Teste",
                        mongodbConnectionString : Configuration.GetConnectionString ("MongoTeste"));
            
            
            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            services.AddScoped<IAuthService, AuthService> ();

            services.AddIdentity<ApplicationUser, ApplicationRole> ()
                .AddMongoDbStores<IMongoDbContext> (mongoDbContext)
                .AddDefaultTokenProviders ();

            services.AddControllers ();
        }
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseOpenApi ();
            app.UseSwaggerUi3 (o => {
                o.DocumentTitle = "SSO-BASE";                
            });

            //app.UseHttpsRedirection ();

            app.UseRouting ();
       

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
            
            app.UseAuthorization ();
            
            // Ativando o middlweare de Health Check
            //app.UseHealthChecks("/status");
            app.UseHealthChecks("/status",
               new HealthCheckOptions()
               {
                   ResponseWriter = async (context, report) =>
                   {
                       var result = JsonConvert.SerializeObject(
                           new
                           {
                               statusApplication = report.Status.ToString(),
                               healthChecks = report.Entries.Select(e => new
                               {
                                   check = e.Key,
                                   ErrorMessage = e.Value.Exception?.Message,
                                   status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                               })
                           });
                       context.Response.ContentType = MediaTypeNames.Application.Json;
                       await context.Response.WriteAsync(result);
                   }
                });
            
            // Gera o endpoint que retornará os dados utilizados no dashboard
            app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI( s => s.AddCustomStylesheet("dotnet.css"));
            
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers();
            });

        }

    }
}