namespace JetMovie.Models.Entities
{
    public class CinemaworldMovie
    {
        public virtual string Id { get; set; }
        public virtual MovieInfo MovieInfo { get; set; }
        public virtual decimal RentPrice { get; set; }
    }
}