using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.Query;
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

        /// <summary>
        /// Return a UserProfile object for the specified username. Because the username is expected
        /// to be unique to the user and is in fact part of the key, the query should only return one 
        /// UserProfile object, not a list.
        /// </summary>
        /// <param name="username">Username associated to UserProfile</param>
        /// <returns>UserProfile for given username</returns>
        /// <exception cref="Exceptions.RepositoryException">Thrown if an error occurs during the query</exception>
        public async Task<Userprofile> FindUserProfileByUsername(string username) {
            var query = "SELECT userprofile.* FROM `userprofile` WHERE username = $username";
            Userprofile user = null;
            var scope = await GetScope();
            try {
                //var result = await scope.QueryAsync<UserProfile>(query, options => options.Parameter("username", username));
                var result = await scope.QueryAsync<Userprofile>(query, new QueryOptions().Parameter("username", username));
                if (result?.MetaData.Status == QueryStatus.Success) {
                    IAsyncEnumerator<Userprofile> e = result.GetAsyncEnumerator();
                    await e.MoveNextAsync();
                    user = e.Current;
                } else {
                    throw new RepositoryException($"Query Failed: {query}");
                }
            } catch (CouchbaseException e) {
                throw new RepositoryException($"Query Failed: {query}", e);
            }
            return user;
        }



        /// <summary>
        /// ???
        /// </summary>
        /// <param name="???">???/param>
        /// <returns>???</returns>
        /// <exception cref="???">???</exception>
        public async Task<Userprofile> InsertUserprofile(Userprofile user){
            var bucket = await base.couchmusicBucketProvider.GetBucketAsync();
            var scope = bucket.Scope("couchify");
            var collection = scope.Collection("userprofile");
            try {
                string key = user.GenKey();
                //TODO: check if already exists             
                await collection.InsertAsync(key, user);
                return user;
            } catch (CouchbaseException ex) {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="???">???/param>
        /// <returns>???</returns>
        /// <exception cref="???">???</exception>
        public async Task DeleteUserKey(string username) {
            var bucket = await base.couchmusicBucketProvider.GetBucketAsync();
            var scope = bucket.Scope("couchify");
            var collection = scope.Collection("userprofile");

            try {
                await collection.RemoveAsync($"userprofile::{username}");
            } catch (DocumentNotFoundException) {
                // Do nothing as the document just doesn't exist yet.
            }
        }
    }
}

