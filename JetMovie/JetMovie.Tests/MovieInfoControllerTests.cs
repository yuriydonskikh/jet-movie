using System.Threading.Tasks;
using JetMovie.Controllers;
using JetMovie.Models.ViewModels;
using JetMovie.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace JetMovie.Tests
{
    public class MovieInfoControllerTests
    {
        private readonly DataBaseContext _context;

        public MovieInfoControllerTests()
        {
            _context = new DataBaseContext();
        }

        [Fact]
        public async Task GetMovies()
        {
            try
            {
                await _context.GetContext();
                var controller = new MovieInfoController(_context.Context, MockHelper.MockLogger<MovieInfoController>());
                var request = new MovieRequest{Page = 2, PageSize = 5};

                var result = await controller.GetMovies(request);
                result.Should().BeOfType<OkObjectResult>();
            }
            finally 
            {
                _context.Close();
            }
        }

        [Fact]
        public async Task GetMovie()
        {
            try
            {
                await _context.GetContext();
                var controller = new MovieInfoController(_context.Context, MockHelper.MockLogger<MovieInfoController>());

                var result = await controller.GetMovie("");
                result.Should().BeOfType<OkObjectResult>();
            }
            finally
            {
                _context.Close();
            }
        }
    }
}
