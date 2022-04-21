
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using Microsoft.Extensions.Configuration;

namespace WebAPI {
    /// <summary>
    /// Base class containing common functionality used by all the repository classes
    /// </summary>
    public class RepositoryBase {
        private readonly INamedBucketProvider _couchmusicBucketProvider;
        private readonly string _scopeName;


        public RepositoryBase(IConfiguration config, INamedBucketProvider bucketProvider) {
            _couchmusicBucketProvider = bucketProvider;
            // Fetch the ScopeName property from the Couchbase section of the appconig.json file
            _scopeName = config["Couchbase:ScopeName"];
        }

        /// <summary>
        /// Returns the bucket associated with the configured BucketProvider
        /// </summary>
        /// <returns>Bucket reference</returns>
        /// <exception cref="Exceptions.RepositoryException">Thrown if BucketProvider fails to return a valid bucket reference</exception>
        public async Task<IBucket> GetBucket() {
            try {
                return await _couchmusicBucketProvider.GetBucketAsync();
            } catch (CouchbaseException ex) {
                throw new RepositoryException("Failed to obtain bucket.", ex);
            }
        }

        /// <summary>
        /// Returns the scope using the _scopeName field obtained from the corresponding
        /// configuration property in the appconfig.json file
        /// </summary>
        /// <returns cref="Couchbase.KeyValue.IScope">Reference to the scope.</returns>
        public async Task<IScope> GetScope() {
            try {
                IBucket bucket = await GetBucket();
                return bucket.Scope(_scopeName);
            } catch (CouchbaseException ex) {
                throw new RepositoryException("Failed to obtain 'couchify' scope.", ex);
            }
        }

        /// <summary>
        /// Returns the requested collection reference. Depends on obtaining a Scope reference.
        /// </summary>
        /// <param name="collectionName">The name of the Couchbase collection to return</param>
        /// <returns cref="Couchbase.KeyValue.ICouchbaseCollection">The reference to the specified collection.</returns>
        public async Task<ICouchbaseCollection> GetCollection(string collectionName) {
            try {
                IScope scope = await GetScope();
                return scope.Collection(collectionName);
            } catch (CouchbaseException ex) {
                throw new RepositoryException($"Failed to obtain collection: {collectionName}", ex);
            }
        }
    }
}
