using System.Threading.Tasks;
using Couchbase;

namespace WebAPI
{
    public interface ICBConnectionManager {
        /// <summary>
        /// Initialize the connection to Couchbase by creating a Cluster connection
        /// based on properties defined in the App.config file.
        /// </summary>
        /// <returns>A reference to the connected Couchbase cluster</returns>
        public Task<ICluster> GetCouchbaseConnection();

        /// <summary>
        /// Using the currently connected cluster reference, fetch and return the bucket using
        /// the bucket name configured in the properties in the App.config file
        /// </summary>
        /// <param name="cluster">A reference to a currently connected Couchbase cluster</param>
        /// <returns></returns>
        public Task<IBucket> GetCouchmusicBucket(ICluster cluster);
    }
}