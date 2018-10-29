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
    public class MovieInfoController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<MovieInfoController> _logger;

        public MovieInfoController(ApplicationDbContext applicationDbContext, ILogger<MovieInfoController> logger)
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
                var result = await _applicationDbContext.GetMovieInfos(movieRequest);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetMovies");
                ModelState.AddErrorToModelState(e);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(string id)
        {
            _logger.LogInformation("GetMovie");
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var cinemaworldResult = await _applicationDbContext.GetCinemaworldMovie(id);
                var filmworldResult = await _applicationDbContext.GetFilmworldMovie(id);

                if (cinemaworldResult == null) return new OkObjectResult(filmworldResult);
                if (filmworldResult == null) return new OkObjectResult(cinemaworldResult);
                if (cinemaworldResult.Price > filmworldResult.Price) return new OkObjectResult(filmworldResult);
                return new OkObjectResult(cinemaworldResult);
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