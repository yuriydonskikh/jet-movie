using System;
using System.Threading.Tasks;
using AutoMapper;
using JetMovie.Data;
using JetMovie.Helpers;
using JetMovie.Models.Entities;
using JetMovie.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JetMovie.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(UserManager<AppUser> userManager, IMapper mapper, ApplicationDbContext applicationDbContext, ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        /// <summary>
        /// Register account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerRegistrationViewModel model)
        {
            _logger.LogInformation("Post");
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var userIdentity = _mapper.Map<AppUser>(model);
                var result = await _userManager.CreateAsync(userIdentity, model.Password);
                if (!result.Succeeded) return new BadRequestObjectResult(ModelState.AddErrorsToModelState(result));
                await _applicationDbContext.AddCustomer(userIdentity.Id);

                return new OkObjectResult("Account created");
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
