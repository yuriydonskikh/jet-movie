using System;
using System.Threading.Tasks;
using JetMovie.Helpers;
using JetMovie.Models;
using JetMovie.Models.Entities;
using JetMovie.Models.ViewModels;
using JetMovie.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace JetMovie.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly ILogger<AuthController> _logger;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _logger = logger;
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Login under registered user
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            _logger.LogInformation("Post");
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var identity = await Tokens.GetClaimsIdentity(_userManager, _jwtFactory, credentials.UserName, credentials.Password);
                if (identity == null) return BadRequest(ModelState.AddErrorToModelState("login_failure", "Invalid username or password."));

                var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Post");
                ModelState.AddErrorToModelState(e);
                return BadRequest(ModelState);
            }
        }
    }
}
