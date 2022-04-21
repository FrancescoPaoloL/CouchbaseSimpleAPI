using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserprofileController : ControllerBase {
        private readonly ILogger<UserprofileController> _logger;
        private readonly UserprofileRepository _repo;

        public UserprofileController(ILogger<UserprofileController> logger) {
            _logger = logger;
        }

        [ActivatorUtilitiesConstructor]
        public UserprofileController(UserprofileRepository repo) {
            _repo = repo;
        }

        [HttpGet] 
        [Route("{Id}")]
        public async Task<Userprofile> Get(string Id) {
            return await _repo.FindById(Id);
        }

        //...  
    }
}


//----
        //It works with (for ex): https://localhost:5001/userprofile/userprofile::aahingeffeteness42037
// [ActivatorUtilitiesConstructor]
        // public UserprofileController(INamedBucketProvider bucketProvider) {
        //     _bucket = bucketProvider.GetBucketAsync().GetAwaiter().GetResult(); 
        // } 

        // [HttpPut]
        // [Route("{username}")]
        // public async Task Put([FromBody]Userprofile userprofile) { 
        //     if (userprofile == null) {
        //         throw new Exception("Error in input data!");
        //     } 

        //     var scope = await _bucket.ScopeAsync("couchify");
        //     var collection = await scope.CollectionAsync("userprofile");
            
        //     await collection.InsertAsync<Userprofile>($"userprofile::{userprofile.username}", userprofile); 
        // }