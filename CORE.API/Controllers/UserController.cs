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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CORE.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Postlogin([FromBody]Newtonsoft.Json.Linq.JObject user)
        {
            var document = BsonSerializer.Deserialize<User>(BsonDocument.Parse(user.ToString(Formatting.Indented)));
            var userObjct = await _userRepository.GetUser(document.username, document.password);

            if (userObjct == null)
                return new NotFoundResult();

            return new ObjectResult(userObjct);
        }
        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Postsignup([FromBody]User user)
        {
            var userObjct = await _userRepository.GetUser(user.username, user.password);

            if (userObjct == null)
                return new NotFoundResult();

            return new ObjectResult(userObjct);
        }

    }
}
