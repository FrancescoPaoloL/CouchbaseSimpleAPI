using System.IO;
using System.Threading.Tasks;
using Couchbase;
using Microsoft.Extensions.Configuration;

namespace WebAPI {
    public class CBConnectionManager: ICBConnectionManager {
        private IConfiguration AppSetting { get; }
        private string ConnectionString;
        private string Username;
        private string Password;
        private string BucketName;

        public CBConnectionManager() {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            ConnectionString = AppSetting["Couchbase:ConnectionString"];
            Username = AppSetting["Couchbase:Username"];
            Password = AppSetting["Couchbase:Password"];
            BucketName = AppSetting["Couchbase:BucketName"];
        }

        /// <summary>
        /// Initialize the connection to Couchbase by creating a Cluster connection
        /// based on properties defined in the App.config file.
        /// </summary>
        /// <returns>A reference to the connected Couchbase cluster</returns>
        public async Task<ICluster> GetCouchbaseConnection() {
            if (string.IsNullOrEmpty(ConnectionString) )
               throw new RepositoryException("ConnectionString can't be null");
            if (string.IsNullOrEmpty(Username) )
               throw new RepositoryException("Username can't be null");
            if (string.IsNullOrEmpty(Password) )
               throw new RepositoryException("Password can't be null");

            return await Cluster.ConnectAsync(ConnectionString, Username, Password);

        }

        /// <summary>
        /// Using the currently connected cluster reference, fetch and return the bucket using
        /// the bucket name configured in the properties in the App.config file
        /// </summary>
        /// <param name="cluster">A reference to a currently connected Couchbase cluster</param>
        /// <returns></returns>
        public async Task<IBucket> GetCouchmusicBucket(ICluster cluster) {
            if (cluster != null) {
                return await cluster.BucketAsync(BucketName);
            } else {
                throw new RepositoryException("Cluster object can't be null");
            }
        }
    }
}