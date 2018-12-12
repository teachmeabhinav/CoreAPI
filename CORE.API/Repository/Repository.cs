using CORE.API.DBcontext;
using CORE.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IShopContext _context;

        public ProductRepository(IShopContext context)
        {
            _context = context;
        }

        async Task IProductRepository.Create(Product entity)
        {
            await _context.Products.InsertOneAsync(entity);
        }

        async Task<bool> IProductRepository.Delete(ObjectId id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        async Task<IEnumerable<Product>> IProductRepository.GetAll()
        {
            var cursor33p = _context
                         .Products;

            var cursor = _context
                          .Products
                          .Find(_ => true);
            return await cursor.ToListAsync();

        }

        Task<Product> IProductRepository.GetProduct(ObjectId id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(m => m.Id, id);
            return _context
                    .Products
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        async Task<bool> IProductRepository.Update(Product entity)
        {
            ReplaceOneResult updateResult =
           await _context
                   .Products
                   .ReplaceOneAsync(
                       filter: g => g.Id == entity.Id,
                       replacement: entity);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }

    public class UserRepository : IUserRepository
    {

        private readonly IShopContext _context;

        public UserRepository(IShopContext context)
        {
            _context = context;
        }
        Task<User> IUserRepository.GetUser(string username, string password)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Where(m => m.username== username && m.password == password);
            return _context
                    .Users
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
    }
}
