using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

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
        //It works with (for ex): https://localhost:5001/userprofile/userprofile::aahingeffeteness42037
        public async Task<Userprofile> Get(string Id) {
            var scope = await _bucket.ScopeAsync("couchify");

            // Get default collection object 
            var collection = await scope.CollectionAsync("userprofile");

            // Get single document using KV search 
            var getResult = await collection.GetAsync(Id); 
            return getResult.ContentAs<Userprofile>(); 
        }

        [HttpPut]
        [Route("{username}")]
        public async Task Put([FromBody]Userprofile userprofile) { 
            if (userprofile == null) {
                throw new Exception("Error in input data!");
            } 

            var scope = await _bucket.ScopeAsync("couchify");
            var collection = await scope.CollectionAsync("userprofile");
            
            await collection.InsertAsync<Userprofile>($"userprofile::{userprofile.username}", userprofile); 
        }
    }
}
