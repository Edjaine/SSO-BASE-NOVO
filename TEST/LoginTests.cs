using System;
using System.Threading.Tasks;
using SSO_BASE_NOVO;
using Xunit;

namespace TEST
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class LoginTests
    {
        private readonly IntegrationTestsFixture<StartupTesteIntegracao> _testsFixture;

        public LoginTests(IntegrationTestsFixture<StartupTesteIntegracao> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Checar se a API está respondendo na rede")]
        [Trait("Categoria", "SSO Autenticação")]
        public async Task API_Responder_DeveRetornarSucesso(){
            //arrage
            var response = await _testsFixture.Client.GetAsync("/Login/Echo");
            response.EnsureSuccessStatusCode();
        }
    }
}
