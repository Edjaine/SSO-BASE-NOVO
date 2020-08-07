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
    public class StartupTesteIntegracao {
        public StartupTesteIntegracao (IConfiguration configuration) {
            Configuration = configuration;
        
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Testing.json")
                .Build();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices (IServiceCollection services) {            

            var mongoDbContext = new MongoDbContext (Configuration.GetConnectionString ("Mongo"),
                "SSO-MODELO");    

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

            app.UseRouting ();      

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
            
            app.UseAuthorization ();                       
               
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}