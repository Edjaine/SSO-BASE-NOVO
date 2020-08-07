using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Newtonsoft.Json;
using SSO_BASE_NOVO;
using TEST.Model;
using Xunit;
using Xunit.Abstractions;

namespace TEST {
    [Collection (nameof (IntegrationApiTestsFixtureCollection))]
    public class LoginTests {
        private readonly IntegrationTestsFixture<StartupTesteIntegracao> _testsFixture;
        private readonly ITestOutputHelper _output;

        public LoginTests (
                            IntegrationTestsFixture<StartupTesteIntegracao> testsFixture ,
                            ITestOutputHelper output) {
            _testsFixture = testsFixture;
            _output = output;
        }

        [Fact (DisplayName = "Checar se a API está respondendo na rede")]
        [Trait ("Categoria", "SSO Autenticação")]
        public async Task API_Responder_DeveRetornarSucesso () {
            //arrage
            var response = await _testsFixture.Client.GetAsync ("/Login/Echo");
            response.EnsureSuccessStatusCode ();
            var responseString = await response.Content.ReadAsStringAsync();
                    
            Assert.Equal("It's Alive!!!!", responseString);
        }
        [Fact(DisplayName = "Criar um novo usuário no sistema")]
        [Trait("Categoria", "SSO Autenticação")]
        public async Task API_CriarNovoUsuario_DeveRetornarSucesso () {
            //arrage
            var content = new Dictionary<string, string> 
                { 
                    { "Usuario", new Faker().Internet.UserName() },
                    { "Senha", new Faker().Internet.Password(10, false, "", "As!1_") },
                    { "Email", new Faker().Internet.Email() }
                };
            var contentJson = JsonConvert.SerializeObject(content);
            _output.WriteLine(contentJson);
            //Act
            var postResponse = await _testsFixture.Client.PostAsync("Login/Criar", new StringContent(contentJson, Encoding.UTF8, "application/json"));
            //Assert
            var resposta =  await postResponse.Content.ReadAsStringAsync();
            _output.WriteLine(resposta);
            dynamic respostaObject = JsonConvert.DeserializeObject(resposta);
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal(respostaObject.errors.Count, 0);
        }
        [Fact(DisplayName = "Criar um novo usuário no sistema")]
        [Trait("Categoria", "SSO Autenticação")]
        public async Task API_CriarNovoUsuario_NãoPodeRetornarSucessoParaUsuarioExistente () {
            //arrage
            var content = new Dictionary<string, string> 
                { 
                    { "Usuario", "EdjaineNovo" } ,
                    { "Senha", "As!1_343423243" },
                    { "Email", "nomepessoa@gmail.com" }
                };
            var contentJson = JsonConvert.SerializeObject(content);            
            var postResponse = await _testsFixture.Client.PostAsync("Login/Criar", new StringContent(contentJson, Encoding.UTF8, "application/json"));
            //Act
            var resposta =  await postResponse.Content.ReadAsStringAsync();  
            //Assert
            _output.WriteLine(resposta);
            dynamic respostaObject = JsonConvert.DeserializeObject(resposta);
            postResponse.EnsureSuccessStatusCode();
            Assert.NotEqual(respostaObject.errors.Count, 0);

        }
    }
}