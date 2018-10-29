using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetMovie.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JetMovie.Data
{
    public partial class ApplicationDbContext
    {
        public async Task<List<MovieViewModel>> GetMovieInfos(MovieRequest request)
        {
            var movies = await Task.Run(() => {
                return _cache.Use(RequestKey(request), () =>
                    MovieInfos.Include(i => i.Genres).ThenInclude(i => i.Genre)
                        .Where(movie => !RequestIsNotEmpty(request) ||
                                        !string.IsNullOrEmpty(request.Actors) && movie.Actors.Contains(request.Actors, StringComparison.InvariantCultureIgnoreCase) ||
                                        !string.IsNullOrEmpty(request.Country) && movie.Country.Equals(request.Country, StringComparison.InvariantCultureIgnoreCase) ||
                                        !string.IsNullOrEmpty(request.Description) &&
                                        movie.Description.Contains(request.Description, StringComparison.InvariantCultureIgnoreCase) ||
                                        !string.IsNullOrEmpty(request.Genre) &&
                                        movie.Genres.Exists(g =>
                                            movie.Genres.Any(gn => gn.Genre.Name.Equals(request.Genre, StringComparison.InvariantCultureIgnoreCase))) ||
                                        !string.IsNullOrEmpty(request.Title) && movie.Title.Contains(request.Title, StringComparison.InvariantCultureIgnoreCase) ||
                                        request.Year > 0 && movie.Released.Year == request.Year
                        ).ToListAsync());
            });

            switch (request.SortBy)
            {
                case SortOptions.ByCountry:
                    movies = movies.OrderBy(o => o.Country).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByDate:
                    movies = movies.OrderBy(o => o.Released).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByTitle:
                    movies = movies.OrderBy(o => o.Title).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByCountryDesc:
                    movies = movies.OrderByDescending(o => o.Country).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByDateDesc:
                    movies = movies.OrderByDescending(o => o.Released).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                case SortOptions.ByTitleDesc:
                    movies = movies.OrderByDescending(o => o.Title).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
                default:
                    movies = movies.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
                    break;
            }

            return _mapper.Map<List<MovieViewModel>>(movies);
        }

        private bool RequestIsNotEmpty(MovieRequest request)
        {
            return !string.IsNullOrEmpty(request.Actors) ||
                   !string.IsNullOrEmpty(request.Country) ||
                   !string.IsNullOrEmpty(request.Description) ||
                   !string.IsNullOrEmpty(request.Genre) ||
                   !string.IsNullOrEmpty(request.Title) ||
                   request.Year > 0;
        }

        private string RequestKey(MovieRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Actors);
            key.Append(request.Country);
            key.Append(request.Description);
            key.Append(request.Genre);
            key.Append(request.Title);
            key.Append(request.Year);
            return key.ToString().ToLower();
        }

        public async Task<MovieViewModel> GetMovieInfo(string id)
        {
            var result = await MovieInfos.Include(i => i.Genres).ThenInclude(i => i.Genre).FirstOrDefaultAsync(i => i.Id == id);
            if (result == null) return null;
            return _mapper.Map<MovieViewModel>(result);
        }

    }
}
