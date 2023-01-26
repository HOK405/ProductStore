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
    public class UnitOfWork : IUnitOfWork
    {
        private Context _db;

        private ProductRepository productRepository;      
        private CustomerRepository customerRepository;
        private CustomerProductRepository customerProductRepository;

        public UnitOfWork(Context db)
        {
            this._db = db;
        }
        public IProductRepository Products
        { 
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(_db);
                return productRepository;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepository(_db);
                return customerRepository;
            }
        }

        public ICustomerProductRepository CustomerProducts
        {
            get
            {
                if (customerProductRepository == null)
                    customerProductRepository = new CustomerProductRepository(_db);
                return customerProductRepository;
            }
        }

        private bool disposed = false;

    }
}
