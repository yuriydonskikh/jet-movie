namespace JetMovie.Models.Entities
{
    public class FilmworldMovie
    {
        public virtual string Id { get; set; }
        public virtual MovieInfo MovieInfo { get; set; }
        public virtual decimal RentPrice { get; set; }
    }
}