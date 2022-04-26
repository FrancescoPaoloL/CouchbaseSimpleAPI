using System.Threading.Tasks;
using Couchbase;
using Couchbase.KeyValue;

namespace WebAPI
{
    public interface IRepositoryBase {
        public Task<IBucket> GetBucket();

        public Task<IScope> GetScope();

        public Task<ICouchbaseCollection> GetCollection(string collectionName);
    }
}
