using System.Threading.Tasks;

namespace WebAPI
{
    public interface IUserprofileRepository {
        public Task<Userprofile> FindById(string key);

        public Task<Userprofile> FindUserProfileByUsername(string username);

        public Task<Userprofile> InsertUserprofile(Userprofile user);
    }
}

