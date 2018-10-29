using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JetMovie.Data;
using JetMovie.Helpers;
using JetMovie.Models.Entities;
using JetMovie.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JetMovie.Controllers
{
    [Produces("application/json"), Authorize(Policy = "ApiUser"), Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<CartController> _logger;
        private readonly ClaimsPrincipal _caller;

        public CartController(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ILogger<CartController> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _caller = httpContextAccessor.HttpContext.User;
        }

        private async Task<Customer> GetCustomer()
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");
            return await _applicationDbContext.GetCustomer(userId.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get");
            try
            {
                var result = await _applicationDbContext.GetCartItems(GetCustomer().Id, false);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get");
                ModelState.AddErrorToModelState(e);
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CartViewModel vm)
        {
            _logger.LogInformation("Post");
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _applicationDbContext.AddCartItem(GetCustomer().Id, vm);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Post");
                ModelState.AddErrorToModelState(e);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            _logger.LogInformation("Put");
            try
            {
                await _applicationDbContext.SetCartItemPaid(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Put");
                ModelState.AddErrorToModelState(e);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete");
            try
            {
                await _applicationDbContext.DeleteCartItem(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Delete");
                ModelState.AddErrorToModelState(e);
                return BadRequest(ModelState);
            }
        }
    }
}