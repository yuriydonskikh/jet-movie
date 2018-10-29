using System.Threading.Tasks;
using AutoFixture;
using JetMovie.Data;
using JetMovie.Models.Entities;
using JetMovie.Models.ViewModels;
using JetMovie.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace JetMovie.Tests.Helpers
{
    public class DataBaseContext
    {
        private readonly Fixture _fixture;

        public SqliteConnection Connection => new SqliteConnection("DataSource=:memory:");
        public ApplicationDbContext Context { get; private set; }

        public DataBaseContext()
        {
            _fixture = new Fixture();
        }
        public async Task GetContext()
        {
            await Connection.OpenAsync();
            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(Connection)
                    .Options;

                Context = new ApplicationDbContext(options, MockHelper.Mapper.Value, new MemoryCacheController());
                await Context.Database.OpenConnectionAsync();
                await Context.Database.MigrateAsync();

                await PrepopulateTables();
            }
            catch
            {
                Connection.Close();
                throw;
            }
        }

        public void Close()
        {
            Context?.Database?.CloseConnection();
            Context?.Dispose();
            Connection?.Close();
        }

        private async Task PrepopulateTables()
        {
            var user = MockHelper.Mapper.Value.Map<AppUser>(_fixture.Create<CustomerRegistrationViewModel>());
            await Context.Users.AddAsync(user);
            await Context.Customers.AddAsync(new Customer { IdentityId = user.Id });
            await Context.SaveChangesAsync();
            await Context.CartItems.AddAsync(new CartItem
            {
                CustomerId = 1,
                Paid = false,
                Price = 1,
                ProvidedBy = ProvidedBy.Cinemaworld
            });
            await Context.SaveChangesAsync();
        }
    }
}
