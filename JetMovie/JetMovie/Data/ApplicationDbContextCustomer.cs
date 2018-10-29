using System.Threading.Tasks;
using JetMovie.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JetMovie.Data
{
    public partial class ApplicationDbContext
    {
        public async Task AddCustomer(string identityId)
        {
            await Customers.AddAsync(new Customer {IdentityId = identityId});
            await SaveChangesAsync();
        }

        public async Task<Customer> GetCustomer(string userId)
        {
            return await Customers.Include(c => c.Identity).FirstOrDefaultAsync(c => c.Identity.Id == userId);
        }
    }
}
