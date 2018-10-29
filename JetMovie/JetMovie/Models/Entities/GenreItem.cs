using System.Collections.Generic;

namespace JetMovie.Models.Entities
{
    public class GenreItem
    {
        public virtual int Id { get; set; }
        public virtual int GenreId { get; set; }
        public virtual string MovieInfoId { get; set; }
        public virtual Genre Genre { get; set; }

    }
}