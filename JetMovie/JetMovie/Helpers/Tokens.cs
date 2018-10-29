using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JetMovie.Models;
using JetMovie.Models.Entities;
using JetMovie.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace JetMovie.Helpers
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName,
            JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var totalSeconds = 0;
            if (jwtOptions?.ValidFor != null)
                totalSeconds = (int)jwtOptions.ValidFor.TotalSeconds;

            var response = new
            {
                id = identity.Claims.FirstOrDefault(c => c.Type == "id")?.Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = totalSeconds
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<ClaimsIdentity> GetClaimsIdentity(UserManager<AppUser> userManager, IJwtFactory jwtFactory,
            string userName, string password)
        {
            var user = await IsLoggedIn(userManager, userName, password);
            if (user != null)
                return await Task.FromResult(jwtFactory.GenerateClaimsIdentity(userName, user.Id));
            return await Task.FromResult<ClaimsIdentity>(null);
        }


        public static async Task<AppUser> IsLoggedIn(UserManager<AppUser> userManager, string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;

            var userToVerify = await userManager.FindByNameAsync(userName);
            if (userToVerify == null) return null;

            return await userManager.CheckPasswordAsync(userToVerify, password) ? userToVerify : null;
        }

    }
}
