using AutoFixture.Xunit2;
using JetMovie.Models.Entities;
using JetMovie.Models.ViewModels;
using JetMovie.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace JetMovie.Tests
{
    public class MappingTests
    {
        [Theory, AutoData]
        public void CustomerRegistrationViewModelToAppUser(CustomerRegistrationViewModel vm)
        {
            var result = MockHelper.Mapper.Value.Map<AppUser>(vm);
            result.Should().BeOfType<AppUser>();
        }

        [Theory, AutoData]
        public void CinemaworldMovieToMovieViewModel(CinemaworldMovie vm)
        {
            var result = MockHelper.Mapper.Value.Map<MovieViewModel>(vm);
            result.Should().BeOfType<MovieViewModel>();
        }

        [Theory, AutoData]
        public void FilmworldMovieToMovieViewModel(FilmworldMovie vm)
        {
            var result = MockHelper.Mapper.Value.Map<MovieViewModel>(vm);
            result.Should().BeOfType<MovieViewModel>();
        }

        [Theory, AutoData]
        public void MovieInfoToMovieViewModel(MovieInfo vm)
        {
            var result = MockHelper.Mapper.Value.Map<MovieViewModel>(vm);
            result.Should().BeOfType<MovieViewModel>();
        }

        [Theory, AutoData]
        public void CartItemToCartViewModel(CartItem vm)
        {
            var result = MockHelper.Mapper.Value.Map<CartViewModel>(vm);
            result.Should().BeOfType<CartViewModel>();
        }
    }
}
