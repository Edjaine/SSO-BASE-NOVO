using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using SSO_BASE_NOVO;
using Xunit;

namespace TEST
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTesteIntegracao>> { }
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly SsoAppFactory<TStartup> Factory;
        public HttpClient Client;
        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions {

            };

            Factory = new SsoAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }
        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
