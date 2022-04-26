using System.Threading.Tasks;
using Xunit;

namespace WebAPI.Tests {
    public class ConnectionTest {
        private CBConnectionManager _sut;

        [Fact]
        [Trait("Category","IntegrationTest")]
        public async Task ConnectionManager_ConnectionSuccess() {
            _sut = new CBConnectionManager();
            var cluster = await _sut.GetCouchbaseConnection();
            var pingResult = await cluster.PingAsync();
            Assert.NotNull(pingResult.Id);
        }

        [Fact]
        [Trait("Category","IntegrationTest")]
        public async Task ConnectionManager_BucketSuccess() {
            _sut = new CBConnectionManager();
            var cluster = await _sut.GetCouchbaseConnection();
            var bucket = await _sut.GetCouchmusicBucket(cluster);
            var pingResult = await bucket.PingAsync();
            Assert.NotNull(pingResult.Id);
        }
    }
}
