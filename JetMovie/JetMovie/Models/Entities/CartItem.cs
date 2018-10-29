using JetMovie.Models.ViewModels;

namespace JetMovie.Models.Entities
{
    public class CartItem
    {
        public virtual int Id { get; set; }
        public virtual MovieInfo Movie { get; set; }
        public virtual ProvidedBy ProvidedBy { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual bool Paid { get; set; }
    }
}