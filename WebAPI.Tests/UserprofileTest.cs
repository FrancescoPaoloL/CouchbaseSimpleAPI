using Couchbase.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace WebAPI.Tests {
    [Collection("Sequential")]
    public class UserprofileTest : TestBase {
        private readonly UserprofileRepository _sut;

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

        [Fact]
        public async Task Userprofile_FindById_Success() {
            Userprofile user = await _sut.FindById("userprofile::aahingeffeteness42037");
            Assert.NotNull(user);
            Assert.Equal("Delores", user.firstName);
            Assert.Equal("Riley", user.lastName);
            Assert.Equal("Mrs", user.title);
            Assert.Equal("delores.riley@hotmail.com", user.email);
            Assert.Equal("aahingeffeteness42037", user.username);
            Assert.Equal("active", user.status);
            Assert.Equal(new DateTime(1983, 07, 12), user.dateOfBirth);
            Assert.Equal("female", user.gender);
            Assert.Equal("warren", user.address.city);
            Assert.Equal("US", user.address.countryCode);
            Assert.Equal("oregon", user.address.state);
            Assert.Equal("6174 elgin st", user.address.street);
            Assert.Equal("63450", user.address.postalCode);
            Assert.Equal("https://randomuser.me/api/portraits/women/88.jpg", user.picture.large);
            Assert.Equal("(943)-434-3888", user.phones[0].number);
            Assert.Equal("home", user.phones[0].type);
            Assert.Equal("Classical Crossover", user.favoriteGenres[0]);
        }


        [Fact]
        public async Task Find_UserProfile_By_Username_Success() {
            Userprofile user = await _sut.FindUserProfileByUsername("stockadeseffusing18695");
            Assert.NotNull(user);
            Assert.Equal("Moreau", user.lastName);
        }
    }
}