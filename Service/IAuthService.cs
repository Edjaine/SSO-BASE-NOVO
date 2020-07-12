using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using sso_base.Models;

namespace sso_base.Service
{
    public interface IAuthService
    {
        Task<IdentityResult> CriaUsuario(string usuario, string email, string senha);
        Task<UserToken> Loga(string usuario, string senha);
         
    }
}