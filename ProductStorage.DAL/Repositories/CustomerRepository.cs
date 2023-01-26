using Microsoft.EntityFrameworkCore;
using ProductStorage.DAL.EF;
using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStorage.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Context _db;
        public CustomerRepository(Context db)
        {
            _db = db;
        }
        public async Task<bool> Create(Customer entity)
        {
            await _db.Customers.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Customer entity)
        {
            Customer customer = _db.Customers.Find(entity.CustomerID);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
            }

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Customer> GetById(int id)
        {
            return await _db.Customers.FirstOrDefaultAsync(x => x.CustomerID == id);
        }

        public async Task<Customer> GetByName(string name)
        {
            return await _db.Customers.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Customer>> Select()
        {
            return await _db.Customers.ToListAsync();
        }

        public async Task<bool> Update(int id, Customer newEntity)
        {
            var product = await _db.Customers.FirstOrDefaultAsync(x => x.CustomerID == id);

            product.Name = newEntity.Name;
            product.Phone = newEntity.Phone;

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
