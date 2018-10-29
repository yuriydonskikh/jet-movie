using JetMovie.Models.ViewModels;
using FluentValidation;

namespace JetMovie.Models.Validations
{
    public class MovieRequestValidator : AbstractValidator<MovieRequest>
    {
        public MovieRequestValidator()
        {
            RuleFor(vm => vm.Page).GreaterThan(0).WithMessage("Page should be greater than 0");
            RuleFor(vm => vm.PageSize).InclusiveBetween(1, 100).WithMessage("PageSize should be between 1 and 100");
        }
    }
}
