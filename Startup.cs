using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDbGenericRepository;
using sso_base.Models;
using sso_base.Service;

namespace SSO_BASE_NOVO {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices (IServiceCollection services) {
            var mongoDbContext = new MongoDbContext (Configuration.GetConnectionString ("DefaultConnection"),
                "SSO-MODELO");

            services.AddSwaggerDocument ();

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
            app.UseSwaggerUi3 ();

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

        }

    }
}