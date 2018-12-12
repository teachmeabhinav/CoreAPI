using CORE.API.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE.API.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetProduct(ObjectId id);
        Task Create(Product entity);
        Task<bool> Update(Product entity);
        Task<bool> Delete(ObjectId id);
      
    }

    public interface IUserRepository
    {
        Task<User> GetUser(string username, string password);

    }
}
