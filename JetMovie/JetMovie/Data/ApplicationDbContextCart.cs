using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetMovie.Models.Entities;
using JetMovie.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JetMovie.Data
{
    public partial class ApplicationDbContext
    {
        public async Task<List<CartViewModel>> GetCartItems(int customerId, bool paid)
        {
            var cartItems = await CartItems.Include(i => i.Movie).Where(i => i.Paid == paid).ToListAsync();
            return _mapper.Map<List<CartViewModel>>(cartItems);
        }

        public async Task AddCartItem(int customerId, CartViewModel vm)
        {
            var item = await CartItems.Include(i => i.Movie).FirstOrDefaultAsync(i => i.Movie.Id == vm.MovieId);
            var movie = await MovieInfos.FirstOrDefaultAsync(i => i.Id == vm.MovieId);
            if (item == null && movie != null)
            {
                await CartItems.AddAsync(new CartItem
                {
                    CustomerId = customerId,
                    Movie = movie,
                    Paid = false,
                    Price = vm.Price,
                    ProvidedBy = vm.ProvidedBy
                });
                await SaveChangesAsync();
            }
        }

        public async Task DeleteCartItem(int id)
        {
            var item = await CartItems.FirstAsync(i => i.Id == id);
            CartItems.Remove(item);
            await SaveChangesAsync();
        }

        public async Task SetCartItemPaid(int id)
        {
            var item = await CartItems.FirstAsync(i => i.Id == id);
            item.Paid = true;
            Update(item);
            await SaveChangesAsync();
        }
    }
}
