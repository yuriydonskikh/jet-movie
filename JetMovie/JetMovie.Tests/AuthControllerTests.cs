using System.Threading.Tasks;
using AutoFixture.Xunit2;
using JetMovie.Controllers;
using JetMovie.Models.Entities;
using JetMovie.Models.ViewModels;
using JetMovie.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace JetMovie.Tests
{
    public class AuthControllerTests
    {
        private readonly DataBaseContext _context;

        public AuthControllerTests()
        {
            _context = new DataBaseContext();
        }

        [Theory, AutoData]
        public async Task Post(AppUser mockUser, CredentialsViewModel mockModel)
        {
            try
            {
                await _context.GetContext();
                var controller = new AuthController(MockHelper.MockUserManager(mockUser), MockHelper.MockJwtFactory(), MockHelper.MockJwtoptions(), MockHelper.MockLogger<AuthController>());
                mockModel.Password = mockModel.Password.Substring(12);

                var result = await controller.Post(mockModel);
                result.Should().BeOfType<OkObjectResult>();
            }
            finally 
            {
                _context.Close();
            }
        }
    }
}
