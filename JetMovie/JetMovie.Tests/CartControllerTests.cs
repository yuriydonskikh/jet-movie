using System.Threading.Tasks;
using AutoFixture.Xunit2;
using JetMovie.Controllers;
using JetMovie.Models.ViewModels;
using JetMovie.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace JetMovie.Tests
{
    public class CartControllerTests
    {
        private readonly DataBaseContext _context;

        public CartControllerTests()
        {
            _context = new DataBaseContext();
        }

        [Fact]
        public async Task Get()
        {
            try
            {
                await _context.GetContext();
                var controller = new CartController(_context.Context, MockHelper.MockHttpContextAccessor(_context.Context),  MockHelper.MockLogger<CartController>());

                var result = await controller.Get();
                result.Should().BeOfType<OkObjectResult>();
            }
            finally 
            {
                _context.Close();
            }
        }

        [Theory, AutoData]
        public async Task Post(CartViewModel vm)
        {
            try
            {
                await _context.GetContext();
                var controller = new CartController(_context.Context, MockHelper.MockHttpContextAccessor(_context.Context), MockHelper.MockLogger<CartController>());

                var result = await controller.Post(vm);
                result.Should().BeOfType<OkResult>();
            }
            finally
            {
                _context.Close();
            }
        }

        [Fact]
        public async Task Put()
        {
            try
            {
                await _context.GetContext();
                var controller = new CartController(_context.Context, MockHelper.MockHttpContextAccessor(_context.Context), MockHelper.MockLogger<CartController>());

                var result = await controller.Put(1);
                result.Should().BeOfType<OkResult>();
            }
            finally
            {
                _context.Close();
            }
        }

        [Fact]
        public async Task Delete()
        {
            try
            {
                await _context.GetContext();
                var controller = new CartController(_context.Context, MockHelper.MockHttpContextAccessor(_context.Context), MockHelper.MockLogger<CartController>());

                var result = await controller.Delete(1);
                result.Should().BeOfType<OkResult>();
            }
            finally
            {
                _context.Close();
            }
        }
    }
}
