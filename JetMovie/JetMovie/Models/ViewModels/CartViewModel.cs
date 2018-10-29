namespace JetMovie.Models.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string MovieId { get; set; }
        public string MovieTitle { get; set; }
        public ProvidedBy ProvidedBy { get; set; }
        public decimal Price { get; set; }
        public bool Paid { get; set; }
    }
}