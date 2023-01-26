using Microsoft.EntityFrameworkCore;
using ProductStorage.DAL.EF;
using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStorage.DAL.Repositories
{
    public class CustomerProductRepository : ICustomerProductRepository
    {
        private readonly Context _db;
        public CustomerProductRepository(Context db)
        {
            _db = db;
        }

        public async Task<bool> Create(CustomerProduct entity)
        {
            await _db.CustomerProducts.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(CustomerProduct entity)
        {
            CustomerProduct customerProduct = await _db.CustomerProducts.FirstOrDefaultAsync(x => x.CustomerId == entity.CustomerId && x.ProductId == entity.ProductId);
            if (customerProduct != null)
            {
                _db.CustomerProducts.Remove(entity);
            }

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<List<CustomerProduct>> GetByCustomerId(int id)
        {
            return await _db.CustomerProducts.Where(x => x.CustomerId == id).ToListAsync();
        }

        public async Task<List<CustomerProduct>> GetByProductId(int id)
        {
            return await _db.CustomerProducts.Where(x => x.ProductId == id).ToListAsync();
        }

        public async Task<List<CustomerProduct>> Select()
        {
            return await _db.CustomerProducts.ToListAsync();
        }

        public async Task<CustomerProduct> GetByAmount(int amount)
        {
            return await _db.CustomerProducts.FirstOrDefaultAsync(x => x.Amount == amount);
        }

        public async Task<CustomerProduct> GetByIds(int customerId, int productId)
        {
            return await _db.CustomerProducts.FirstOrDefaultAsync(x => x.CustomerId == customerId && x.ProductId == productId);
        }

        public async Task<bool> Update(int customerId, int productId, int newAmount)
        {
            var product = await _db.CustomerProducts.FirstOrDefaultAsync(x => x.CustomerId == customerId && x.ProductId == productId);

            product.Amount = newAmount;

            await _db.SaveChangesAsync();

            return true;
        }


    }
}
