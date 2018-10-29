using System;
using System.Threading.Tasks;
using JetMovie.Data;
using JetMovie.Helpers;
using JetMovie.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JetMovie.Controllers
{
    [Produces("application/json"), Authorize(Policy = "ApiUser"), Route("api/[controller]/[action]")]
    [ApiController]
    public class FilmworldController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<FilmworldController> _logger;

        public FilmworldController(ApplicationDbContext applicationDbContext, ILogger<FilmworldController> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] MovieRequest movieRequest)
        {
            _logger.LogInformation("GetMovies");
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await _applicationDbContext.GetFilmworldMovies(movieRequest);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetMovies");
                ModelState.AddErrorToModelState(e);
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMovie(string id)
        {
            _logger.LogInformation("GetMovie");
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await _applicationDbContext.GetFilmworldMovie(id);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetMovie");
                ModelState.AddErrorToModelState(e);
                return BadRequest(ModelState);
            }
        }
    }
}