using Microsoft.AspNetCore.Identity;

namespace JetMovie.Models.Entities
{
    // Add profile data for application users by adding properties to this class
    public class AppUser : IdentityUser
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}
