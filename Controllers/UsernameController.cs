using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UsernameController : ControllerBase {
        private readonly ILogger<UsernameController> _logger;
        private readonly IBucket _bucket;  

        public UsernameController(ILogger<UsernameController> logger) {
            _logger = logger;
        }

        [ActivatorUtilitiesConstructor]
        public UsernameController(INamedBucketProvider bucketProvider) {
            _bucket = bucketProvider.GetBucketAsync().GetAwaiter().GetResult(); 
        } 


        [HttpGet] 
        [Route("{Id}")] 
        public async Task<Username> Get(string Id) {
            //Id = "userprofile::aahingeffeteness42037";

            // Get default collection object 
            var collection = await _bucket.DefaultCollectionAsync(); 

            // Get single document using KV search 

            var getResult = await collection.GetAsync(Id); 
            return getResult.ContentAs<Username>(); 
        }
    }
}
