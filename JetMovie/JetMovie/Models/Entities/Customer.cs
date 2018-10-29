namespace JetMovie.Models.Entities
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string IdentityId { get; set; }
        public virtual AppUser Identity { get; set; }
    }
}
