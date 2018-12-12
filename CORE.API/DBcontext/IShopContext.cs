using CORE.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE.API.DBcontext
{
    public class IShopContext
    {
       public virtual IMongoCollection<Product> Products { get; }
       public virtual IMongoCollection<User> Users { get; }
    }
}
