using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDbGenericRepository;
using sso_base.Models;
using sso_base.Service;
using SSO_BASE_NOVO.Dto;

namespace sso_base.Controllers {

    [ApiController]
    [Route ("[controller]")]
    public class LoginController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public LoginController (IConfiguration configuration,
            IAuthService authService) {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost ("Criar")]
        public async Task<IActionResult> CreateUser ([FromBody] UsuarioDto usuarioDto) {

            await _authService.CriaUsuario (usuarioDto.Usuario, usuarioDto.Email, usuarioDto.Senha);
            return Ok ("Criado com sucesso");
        }

        [HttpGet("Logar")]
        public async Task<IActionResult> Login (string usuario, string senha) {

            try {
                return Ok (await _authService.Loga (usuario, senha));
            } catch (Exception ex) {
                ModelState.AddModelError (string.Empty, ex.Message.ToString ());
                return BadRequest (ModelState);
            }

        }

    }
}