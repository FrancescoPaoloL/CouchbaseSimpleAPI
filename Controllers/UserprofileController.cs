using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserprofileController : ControllerBase {
        private readonly ILogger<UserprofileController> _logger;
        private readonly IBucket _bucket;  

        public UserprofileController(ILogger<UserprofileController> logger) {
            _logger = logger;
        }

        [ActivatorUtilitiesConstructor]
        public UserprofileController(INamedBucketProvider bucketProvider) {
            _bucket = bucketProvider.GetBucketAsync().GetAwaiter().GetResult(); 
        } 


        [HttpGet] 
        [Route("{Id}")] 
        public async Task<Username> Get(string Id) {
            Id = "userprofile::aahingeffeteness42037";
            //Id = "00011b74-12be-4e60-abbf-b1c8b9b40bfe";
            
            var scope = await _bucket.ScopeAsync("couchify");

            // Get default collection object 
            var collection = await scope.CollectionAsync("userprofile");

            // Get single document using KV search 

            var getResult = await collection.GetAsync(Id); 
            return getResult.ContentAs<Username>(); 
        }
    }
}
