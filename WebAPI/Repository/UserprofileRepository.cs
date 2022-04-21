using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace WebAPI {
    public class UserprofileRepository : RepositoryBase {
        public UserprofileRepository(IConfiguration config, INamedBucketProvider bucketProvider)
            : base(config, bucketProvider) {
        }

        /// <summary>
        /// Find an entity by the specified key and return a reference to an 
        /// instance of the specified type. 
        /// </summary>
        /// <param name="key"> Unique ID (key) of the entity</param>
        /// <returns>Reference to an instance of the UserProfile that corresponds
        /// to the ID</returns>
        /// <exception cref="Exceptions.RepositoryException">Thrown if an error occurs during the get</exception>
        public async Task<Userprofile> FindById(string key) {
            var collection = await GetCollection("userprofile");
            try {
                // Perform an asynchronous Get operation
                var result = await collection.GetAsync(key);
                return result.ContentAs<Userprofile>();
            } catch (DocumentNotFoundException ex) {
                throw new RepositoryException($"Unable to locate document for key: {key}", ex);
            } catch (CouchbaseException ex) {
                throw new RepositoryException($"Error attempting to perform GetAsync() for key: {key}", ex);
            }
        }

    }
}




//----
        // /// <summary>
        // /// Update an existing instance of the UserProfile in the repository.
        // /// </summary>
        // /// <param name="entity">Source entity to be persisted</param>
        // /// <returns>Reference to the entity that has been persisted</returns>
        // /// <exception cref="Exceptions.RepositoryException">Thrown if an error occurs during the update</exception>
        // public async Task<UserProfile> Update(UserProfile entity)
        // {
        //     var collection = await GetCollection("userprofile");
        //     try
        //     {
        //         // Perform an asynchronous Get operation
        //         await collection.ReplaceAsync(entity.GenKey(), entity);
        //         return entity;
        //     }
        //     catch (DocumentNotFoundException ex)
        //     {
        //         throw new RepositoryException($"Unable to locate document for key: {entity.GenKey()}", ex);
        //     }
        //     catch (CouchbaseException ex)
        //     {
        //         throw new RepositoryException("Failure while trying to update UserProfile", ex);
        //     }
        // }

        // /// <summary>
        // /// Return a UserProfile object for the specified username. Because the username is expected
        // /// to be unique to the user and is in fact part of the key, the query should only return one 
        // /// UserProfile object, not a list.
        // /// </summary>
        // /// <param name="username">Username associated to UserProfile</param>
        // /// <returns>UserProfile for given username</returns>
        // /// <exception cref="Exceptions.RepositoryException">Thrown if an error occurs during the query</exception>
        // public async Task<UserProfile> FindUserProfileByUsername(string username)
        // {
        //     var query = "select userprofile.* from `userprofile` where username = $username";
        //     UserProfile user = null;
        //     var scope = await GetScope();
        //     try
        //     {
        //         //var result = await scope.QueryAsync<UserProfile>(query, options => options.Parameter("username", username));
        //         var result = await scope.QueryAsync<UserProfile>(query, new QueryOptions().Parameter("username", username));
        //         if (result?.MetaData.Status == QueryStatus.Success)
        //         {
        //             IAsyncEnumerator<UserProfile> e = result.GetAsyncEnumerator();
        //             await e.MoveNextAsync();
        //             user = e.Current;
        //         }
        //         else
        //         {
        //             throw new RepositoryException($"Query Failed: {query}");
        //         }
        //     }
        //     catch (CouchbaseException e)
        //     {
        //         throw new RepositoryException($"Query Failed: {query}", e);
        //     }
        //     return user;
        // }

        // /// <summary>
        // /// Given an entity provided, which will be used to update the UserProfile,
        // /// get the existing entry for the purposes of obtaining the current CAS value.
        // /// Then, attempt to make an update, allowing for potential other writers
        // /// </summary>
        // /// <param name="key">Key for UserProfile entry to update</param>
        // /// <param name="password">Password to replace (yes, it should be properly hashed)</param>
        // /// <returns>The updated UserProfile value</returns>
        // /// <exception cref="Exceptions.RepositoryException">Thrown if update using Optimistic Lock fails</exception>
        // public async Task<UserProfile> UpdatePasswordWithOptimisticLock(string key, string password)
        // {
        //     var collection = await GetCollection("userprofile");
        //     var maxTries = 5;
        //     for (var i = 0; i < maxTries; i++)
        //     {
        //         var getResult = await collection.GetAsync(key);
        //         UserProfile user = getResult.ContentAs<UserProfile>();
        //         var originalCas = getResult.Cas;
        //         user.Pwd = password;

        //         try
        //         {
        //             await collection.ReplaceAsync(key, user, opts => opts.Cas(originalCas));
        //             return user;
        //         }
        //         catch (CasMismatchException)
        //         {
        //             Thread.Sleep(1000);
        //         }
        //     }
        //     throw new RepositoryException("Failed to update entity with optimistic locking");

        // }