using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.API.Models;
using CORE.API.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

namespace CORE.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await _productRepository.GetAll());
        }

        // GET: api/Product/name
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productRepository.GetProduct(ObjectId.Parse(id));

            if (product == null)
                return new NotFoundResult();

            return new ObjectResult(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Newtonsoft.Json.Linq.JObject product)
        {
            var document = BsonSerializer.Deserialize<Product>(BsonDocument.Parse(product.ToString(Formatting.Indented)));
            await _productRepository.Create(document);
            return new OkObjectResult(document);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Newtonsoft.Json.Linq.JObject product, string id)
        {
            var document = BsonSerializer.Deserialize<Product>(BsonDocument.Parse(product.ToString(Formatting.Indented)));
            var productFromDb = await _productRepository.GetProduct(ObjectId.Parse(id));

            if (productFromDb == null)
                return new NotFoundResult();

            document.Id = productFromDb.Id;
          // productFromDb.name=document.

            await _productRepository.Update(document);

            return new OkObjectResult(product);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var productFromDb = await _productRepository.GetProduct(ObjectId.Parse(id));

            if (productFromDb == null)
                return new NotFoundResult();

            await _productRepository.Delete(ObjectId.Parse(id));

            return new OkResult();
        }
    }
}
