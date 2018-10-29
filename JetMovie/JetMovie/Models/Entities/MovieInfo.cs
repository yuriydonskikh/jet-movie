using System;
using System.Collections.Generic;

namespace JetMovie.Models.Entities
{
    public class MovieInfo
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime Released { get; set; }
        public virtual List<GenreItem> Genres { get; set; }
        public virtual string Description { get; set; }
        public virtual string Poster { get; set; }
        public virtual string Country { get; set; }
        public virtual string Actors { get; set; }
    }
}