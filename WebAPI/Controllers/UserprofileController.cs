using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


namespace WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserprofileController : Controller {
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
