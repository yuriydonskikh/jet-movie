namespace JetMovie.Models.ViewModels
{
    public class MovieRequest
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Actors { get; set; }
        public SortOptions SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}