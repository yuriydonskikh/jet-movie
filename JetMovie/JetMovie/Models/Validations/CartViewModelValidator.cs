using JetMovie.Models.ViewModels;
using FluentValidation;

namespace JetMovie.Models.Validations
{
    public class CartViewModelValidator : AbstractValidator<CartViewModel>
    {
        public CartViewModelValidator()
        {
            RuleFor(vm => vm.MovieId).NotEmpty().WithMessage("MovieId shoud be provided");
            RuleFor(vm => vm.Price).GreaterThan(0).WithMessage("Price have to be greater than 0");
        }
    }
}
