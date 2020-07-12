using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace sso_base.Models
{
    [CollectionName("Users")]
    public class ApplicationUser: MongoIdentityUser<System.Guid>
    {
    public ApplicationUser() : base()
	{
	}

	public ApplicationUser(string userName, string email) : base(userName, email)
	{
	}
    }
}