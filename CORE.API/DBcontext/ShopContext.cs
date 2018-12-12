using CORE.API.Comman;
using CORE.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace CORE.API.DBcontext
{
    public class ShopContext : IShopContext
    {
        private readonly IMongoDatabase _db;

        public ShopContext(IOptions<Settings> options, IMongoClient client)
        {
             _db = client.GetDatabase(options.Value.Database);
            //  setCollection();
            var aaa = _db.GetCollection<Product>("Products");
        }


        public override IMongoCollection<Product> Products => _db.GetCollection<Product>("products");
        public override IMongoCollection<User> Users => _db.GetCollection<User>("users");

    }
  
}
