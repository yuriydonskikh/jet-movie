using System.Threading.Tasks;
using JetMovie.Controllers;
using JetMovie.Models.ViewModels;
using JetMovie.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace JetMovie.Tests
{
    public class FilmworldControllerTests
    {
        private readonly DataBaseContext _context;

        public FilmworldControllerTests()
        {
            _context = new DataBaseContext();
        }

        [Fact]
        public async Task GetMovies()
        {
            try
            {
                await _context.GetContext();
                var controller = new FilmworldController(_context.Context, MockHelper.MockLogger<FilmworldController>());
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
                var controller = new FilmworldController(_context.Context, MockHelper.MockLogger<FilmworldController>());

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
