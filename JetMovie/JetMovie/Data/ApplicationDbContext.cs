using AutoMapper;
using JetMovie.Models.Entities;
using JetMovie.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JetMovie.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private readonly IMapper _mapper;
        private readonly ICacheController _cache;

        public ApplicationDbContext(DbContextOptions options, IMapper mapper, ICacheController cache)
            : base(options)
        {
            _mapper = mapper;
            _cache = cache;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CinemaworldMovie> CinemaworldMovies { get; set; }
        public DbSet<FilmworldMovie> FilmworldMovies { get; set; }
        public DbSet<MovieInfo> MovieInfos { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreItem> GenreItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
