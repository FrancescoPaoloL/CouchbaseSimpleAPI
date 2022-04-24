using Xunit;
using System.Threading.Tasks;
using Couchbase.Extensions.DependencyInjection;

namespace WebAPI.Tests {
    public class TestBase : IAsyncLifetime {

        private readonly INamedBucketProvider _couchmusicBucketProvider;

        public TestBase(INamedBucketProvider couchmusincBucketProvider) {
            _couchmusicBucketProvider = couchmusincBucketProvider;
        }

        public Task InitializeAsync() {
            return Task.CompletedTask;
        }

        public Task DisposeAsync() {
            return Task.CompletedTask;
        }
    }
}