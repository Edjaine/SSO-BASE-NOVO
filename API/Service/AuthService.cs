using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sso_base.Models;

namespace sso_base.Service {
    public class AuthService : IAuthService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;
        private readonly IConfiguration _configuration;

        public AuthService (UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager, 
                            IConfiguration configuration) {
            _userManager = userManager;
            _singInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> CriaUsuario (string usuario, string email, string senha) {
            var user = new ApplicationUser { UserName = usuario, Email = usuario };
            return await _userManager.CreateAsync(user, senha);            
        }

        public async Task<UserToken> Loga (string usuario, string senha) {

            var result = await _singInManager.PasswordSignInAsync (usuario, senha, false, false);

            if (result.Succeeded)
                return BuildToken (usuario, senha);
                
            throw new Exception("Ocorreu um erro no login");
        }

        private UserToken BuildToken (string usuario, string senha) {

            var claims = new [] {
                new Claim (JwtRegisteredClaimNames.UniqueName, senha),
                new Claim ("meuValor", "oque voce quiser"),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ())
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_configuration["JWT:key"]));
            var cread = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours (1);

            var token = new JwtSecurityToken (
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: cread
            );

            return new UserToken () {
                Token = new JwtSecurityTokenHandler ().WriteToken (token),
                    Expiration = expiration
            };

        }
    }
}