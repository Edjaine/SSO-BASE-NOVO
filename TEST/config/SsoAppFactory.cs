using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TEST
{
    public class SsoAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.UseStartup<TStartup>();
            builder.UseEnvironment("Testing");
        }
                                      
    }
}
