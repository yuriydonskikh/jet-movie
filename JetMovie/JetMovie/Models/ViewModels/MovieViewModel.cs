using System;
using System.Collections.Generic;

namespace JetMovie.Models.ViewModels
{
    public class MovieViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Released { get; set; }
        public List<string> Genres { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public string Country { get; set; }
        public string Actors { get; set; }
        public ProvidedBy ProvidedBy { get; set; }
        public decimal Price { get; set; }
    }
}