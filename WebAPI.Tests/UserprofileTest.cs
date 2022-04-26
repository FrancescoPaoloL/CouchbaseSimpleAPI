using Couchbase.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace WebAPI.Tests {
    [TestCaseOrderer("WebAPI.Tests.PriorityOrderer", "WebAPI.Tests")]
    public class UserprofileTest : TestBase {
        private readonly UserprofileRepository _sut;
        private readonly string _username = "aurorasmith42037";

        public UserprofileTest(INamedBucketProvider namedBucketProvider, UserprofileRepository userProfileRepository) : base(namedBucketProvider) {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                 path: "appsettings.json",
                 optional: false,
                 reloadOnChange: true)
           .Build();
           _sut = new UserprofileRepository(config, namedBucketProvider);
        }


        [Fact, TestPriority(0)]
        [Trait("Category","IntegrationTest")]
        public async Task Userprofile_Create_Success() {           
            var user = await Task.Run(() => CreateUsername(_username));
            user.Should().NotBe(null);
            await _sut.InsertUserprofile(user);
        }


        [Fact, TestPriority(1)]
        [Trait("Category","IntegrationTest")]
        public async Task Userprofile_FindById_Success() {
            var userMock = await Task.Run(() => CreateUsername(_username));
            Userprofile user = await _sut.FindById($"userprofile::{_username}");

            user.Should().NotBe(null);
            user.Should().BeOfType<Userprofile>();
            user.Should().BeEquivalentTo(userMock);
            
        }

        [Fact, TestPriority(2)]
        [Trait("Category","IntegrationTest")]
        public async Task Delete_UserProfile_By_Username_Success() {
            await _sut.DeleteUserKey(_username);
        }
    }
}