using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using JetMovie.Data;
using JetMovie.Models;
using JetMovie.Models.Entities;
using JetMovie.Models.ViewModels;
using JetMovie.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace JetMovie.Tests.Helpers
{
    public static class MockHelper
    {
        public static Lazy<IMapper> Mapper
        {
            get
            {
                return new Lazy<IMapper>(() =>
                {
                    var config = new MapperConfiguration(cfg => cfg.AddProfile(new ViewModelMapper()));
                    return new Mapper(config);
                });
            }
        }

        public static UserManager<AppUser> GetUserManager(IUserStore<AppUser> store)
        {
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions {Lockout = {AllowedForNewUsers = false}};
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<AppUser>>();
            var validator = new Mock<IUserValidator<AppUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<AppUser>> {new PasswordValidator<AppUser>()};
            var userManager = new UserManager<AppUser>(store, options.Object, new PasswordHasher<AppUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<AppUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<AppUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }

        public static UserManager<AppUser> MockUserManager(AppUser appUser)
        {
            var store = new Mock<IUserStore<AppUser>>();
            var userManager = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(appUser));
            userManager.Setup(m => m.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            return userManager.Object;
        }

        public static IJwtFactory MockJwtFactory()
        {
            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(m => m.GenerateClaimsIdentity(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ClaimsIdentity());
            return mockJwtFactory.Object;
        }

        public static IOptions<JwtIssuerOptions> MockJwtoptions()
        {
            return new Mock<IOptions<JwtIssuerOptions>>().Object;
        }

        public static IHttpContextAccessor MockHttpContextAccessor(ApplicationDbContext context)
        {
            var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new[]
            {
                new Claim("id", context.Customers.First().IdentityId), 
            };
            mockClaimsPrincipal.Setup(m => m.Claims).Returns(claims);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.User).Returns(mockClaimsPrincipal.Object);
            var result = new Mock<IHttpContextAccessor>();
            result.Setup(m => m.HttpContext).Returns(mockHttpContext.Object);
            return result.Object;
        }

        public static ILogger<T> MockLogger<T>()
        {
            return new Mock<ILogger<T>>().Object;
        }
    }
}
