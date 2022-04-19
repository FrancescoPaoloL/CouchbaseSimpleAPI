using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PlaylistController : ControllerBase {
        private readonly ILogger<PlaylistController> _logger;
        private readonly IBucket _bucket;  

        public PlaylistController(ILogger<PlaylistController> logger) {
            _logger = logger;
        }

        [ActivatorUtilitiesConstructor]
        public PlaylistController(INamedBucketProvider bucketProvider) {
            _bucket = bucketProvider.GetBucketAsync().GetAwaiter().GetResult(); 
        } 


        [HttpGet] 
        [Route("{Id}")] 
        public async Task<Username> Get(string Id) {
            Id = "playlist::00011b74-12be-4e60-abbf-b1c8b9b40bfe";
            //Id = "00011b74-12be-4e60-abbf-b1c8b9b40bfe";
            
            var scope = await _bucket.ScopeAsync("couchify");

            // Get default collection object 
            var collection = await scope.CollectionAsync("playlist"); //   .DefaultCollectionAsync(); 

            // Get single document using KV search 

            var getResult = await collection.GetAsync(Id); 
            return getResult.ContentAs<Username>(); 
        }
    }
}
