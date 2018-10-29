using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JetMovie.Models.Entities;

namespace JetMovie.Models.ViewModels
{
    public class ViewModelMapper:Profile
    {
        public ViewModelMapper()
        {
            CreateMap<CustomerRegistrationViewModel, AppUser>()
                .ForMember(dst => dst.UserName, src => src.MapFrom(vm => vm.Email));

            CreateMap<CinemaworldMovie, MovieViewModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.MovieInfo.Id))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.MovieInfo.Title))
                .ForMember(dst => dst.Released, opt => opt.MapFrom(src => src.MovieInfo.Released))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.MovieInfo.Description))
                .ForMember(dst => dst.Poster, opt => opt.MapFrom(src => src.MovieInfo.Poster))
                .ForMember(dst => dst.Country, opt => opt.MapFrom(src => src.MovieInfo.Country))
                .ForMember(dst => dst.Actors, opt => opt.MapFrom(src => src.MovieInfo.Actors))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.RentPrice))
                .ForMember(dst => dst.ProvidedBy, opt => opt.MapFrom(src => ProvidedBy.Cinemaworld))
                .ForMember(dst => dst.Genres,
                    opt => opt.MapFrom(src => new List<string>(src.MovieInfo.Genres.Select(i => i.Genre.Name))));

            CreateMap<FilmworldMovie, MovieViewModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.MovieInfo.Id))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.MovieInfo.Title))
                .ForMember(dst => dst.Released, opt => opt.MapFrom(src => src.MovieInfo.Released))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.MovieInfo.Description))
                .ForMember(dst => dst.Poster, opt => opt.MapFrom(src => src.MovieInfo.Poster))
                .ForMember(dst => dst.Country, opt => opt.MapFrom(src => src.MovieInfo.Country))
                .ForMember(dst => dst.Actors, opt => opt.MapFrom(src => src.MovieInfo.Actors))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.RentPrice))
                .ForMember(dst => dst.ProvidedBy, opt => opt.MapFrom(src => ProvidedBy.FilmWorld))
                .ForMember(dst => dst.Genres,
                    opt => opt.MapFrom(src => new List<string>(src.MovieInfo.Genres.Select(i => i.Genre.Name))));

            CreateMap<MovieInfo, MovieViewModel>()
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => 0))
                .ForMember(dst => dst.ProvidedBy, opt => opt.MapFrom(src => ProvidedBy.None))
                .ForMember(dst => dst.Genres,
                    opt => opt.MapFrom(src => new List<string>(src.Genres.Select(i => i.Genre.Name))));

            CreateMap<CartItem, CartViewModel>()
                .ForMember(dst => dst.MovieId, opt => opt.MapFrom(src => src.Movie.Id))
                .ForMember(dst => dst.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title));

        }
    }
}
