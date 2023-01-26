using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Repositories;
using ProductStorage.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.DAL.Interfaces
{
    public interface IUnitOfWork 
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        ICustomerProductRepository CustomerProducts { get;}
    }
}
