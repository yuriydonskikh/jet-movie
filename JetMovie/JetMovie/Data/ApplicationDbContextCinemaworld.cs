using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetMovie.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JetMovie.Data
{
    public partial class ApplicationDbContext
    {
        public async Task<List<MovieViewModel>> GetCinemaworldMovies(MovieRequest request)
        {
            var movies = await Task.Run(() => {
                return _cache.Use(RequestKey(request), () =>
                    CinemaworldMovies.Include(i => i.MovieInfo).ThenInclude(i => i.Genres).ThenInclude(i => i.Genre)
                        .Where(movie => !RequestIsNotEmpty(request) ||
                                        !string.IsNullOrEmpty(request.Actors) && movie.MovieInfo.Actors.Contains(request.Actors, StringComparison.InvariantCultureIgnoreCase) ||
                                        !string.IsNullOrEmpty(request.Country) && movie.MovieInfo.Country.Equals(request.Country, StringComparison.InvariantCultureIgnoreCase) ||
                                        !string.IsNullOrEmpty(request.Description) &&
                                        movie.MovieInfo.Description.Contains(request.Description, StringComparison.InvariantCultureIgnoreCase) ||
                                        !string.IsNullOrEmpty(request.Genre) &&
                                        movie.MovieInfo.Genres.Exists(g =>
                                            movie.MovieInfo.Genres.Any(gn => gn.Genre.Name.Equals(request.Genre, StringComparison.InvariantCultureIgnoreCase))) ||
                                        !string.IsNullOrEmpty(request.Title) && movie.MovieInfo.Title.Contains(request.Title, StringComparison.InvariantCultureIgnoreCase) ||
                                        request.Year > 0 && movie.MovieInfo.Released.Year == request.Year
                        ).ToListAsync());
            });

            switch (request.SortBy)
            {
                case SortOptions.ByCountry:
                    movies = movies.OrderBy(o => o.MovieInfo.Country).Skip((request.Page -1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByDate:
                    movies = movies.OrderBy(o => o.MovieInfo.Released).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByTitle:
                    movies = movies.OrderBy(o => o.MovieInfo.Title).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByCountryDesc:
                    movies = movies.OrderByDescending(o => o.MovieInfo.Country).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByDateDesc:
                    movies = movies.OrderByDescending(o => o.MovieInfo.Released).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByTitleDesc:
                    movies = movies.OrderByDescending(o => o.MovieInfo.Title).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                default:
                    movies = movies.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
            }

            return _mapper.Map<List<MovieViewModel>>(movies);
        }

        public async Task<MovieViewModel> GetCinemaworldMovie(string id)
        {
            var result = await CinemaworldMovies.Include(i => i.MovieInfo).ThenInclude(i=>i.Genres).ThenInclude(i=>i.Genre).FirstOrDefaultAsync(i => i.MovieInfo.Id == id);
            if (result == null) return null;
            return _mapper.Map<MovieViewModel>(result);
        }

    }
}
