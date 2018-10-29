using JetMovie.Models.Validations;
using FluentValidation.TestHelper;
using Xunit;

namespace JetMovie.Tests
{
    public class ValidatorsTests
    {
        [Fact]
        public void CredentialsViewModelValidator()
        {
            var validator = new CredentialsViewModelValidator();
            validator.ShouldHaveValidationErrorFor(e => e.UserName, string.Empty);
            validator.ShouldHaveValidationErrorFor(e => e.Password, string.Empty);
            validator.ShouldNotHaveValidationErrorFor(e => e.Password, "123456");
            validator.ShouldNotHaveValidationErrorFor(e => e.Password, "123456789");
            validator.ShouldNotHaveValidationErrorFor(e => e.Password, "123456789112");
            validator.ShouldHaveValidationErrorFor(e => e.Password, "1234567891123");
        }

        [Fact]
        public void MovieRequestValidator()
        {
            var validator = new MovieRequestValidator();
            validator.ShouldNotHaveValidationErrorFor(e=>e.Page, 1);
            validator.ShouldHaveValidationErrorFor(e => e.Page, 0);
            validator.ShouldNotHaveValidationErrorFor(e => e.PageSize, 1);
            validator.ShouldNotHaveValidationErrorFor(e => e.PageSize, 100);
            validator.ShouldHaveValidationErrorFor(e => e.PageSize, 0);
            validator.ShouldHaveValidationErrorFor(e => e.PageSize, 101);

        }

        [Fact]
        public void CartViewModelValidator()
        {
            var validator = new CartViewModelValidator();
            validator.ShouldNotHaveValidationErrorFor(e => e.MovieId, "aaa111");
            validator.ShouldHaveValidationErrorFor(e => e.MovieId, string.Empty);
            validator.ShouldNotHaveValidationErrorFor(e => e.Price, 1M);
            validator.ShouldHaveValidationErrorFor(e => e.Price, 0);
        }
    }
}
