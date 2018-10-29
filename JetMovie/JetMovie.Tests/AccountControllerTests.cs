using System.Threading.Tasks;
using AutoFixture.Xunit2;
using JetMovie.Controllers;
using JetMovie.Models.Entities;
using JetMovie.Models.ViewModels;
using JetMovie.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace JetMovie.Tests
{
    public class AccountControllerTests
    {
        private readonly DataBaseContext _context;

        public AccountControllerTests()
        {
            _context = new DataBaseContext();
        }

        [Theory, AutoData]
        public async Task Post(CustomerRegistrationViewModel mockModel)
        {
            try
            {
                await _context.GetContext();
                var controller = new AccountsController(MockHelper.GetUserManager(new UserStore<AppUser>(_context.Context)), MockHelper.Mapper.Value, _context.Context, MockHelper.MockLogger<AccountsController>());

                var result = await controller.Post(mockModel);
                result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("Account created");
            }
            finally 
            {
                _context.Close();
            }
        }
    }
}
