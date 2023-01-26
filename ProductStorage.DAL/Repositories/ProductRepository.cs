using Microsoft.EntityFrameworkCore;
using ProductStorage.DAL.EF;
using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _db;
        public ProductRepository(Context db)
        {
            _db = db;
        }

        public async Task<bool> Create(Product entity)
        {
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Product entity)
        {
            Product product = _db.Products.Find(entity.ProductId);
            if (product != null)
            {
                _db.Products.Remove(product);
            }

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Product> GetById(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<Product> GetByName(string name)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Product>> Select()
        {
            return await _db.Products.ToListAsync();
        }
        public async Task<bool> Update(int id, Product newEntity)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            product.Name = newEntity.Name;
            product.Price = newEntity.Price;
            product.Amount = newEntity.Amount;

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
