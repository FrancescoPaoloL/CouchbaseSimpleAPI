using Xunit;
using System.Threading.Tasks;
using Couchbase.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace WebAPI.Tests {
    public class TestBase : IAsyncLifetime {
        private readonly INamedBucketProvider _couchmusicBucketProvider;

        public TestBase(INamedBucketProvider couchmusincBucketProvider) {
            _couchmusicBucketProvider = couchmusincBucketProvider;
        }

        protected Userprofile CreateUsername(string username) {
            List<string> favGenred = new List<string>();
            favGenred.Add("Classical Crossover");
            favGenred.Add("Contemporary Blues");
            favGenred.Add("French Pop");
            favGenred.Add("Progressive Bluegrass");

            Phones phoneData = new Phones();
            phoneData.number = "123";
            phoneData.type = "Home";
            phoneData.verified = new DateTime(2022, 04, 01).Date;

            List<Phones> lstPhones = new List<Phones>();
            lstPhones.Add(phoneData);

            
            Pictures pictures = new Pictures();
            pictures.thumbnail = "https://randomuser.me/api/portraits/women/88.jpg";
            pictures.medium = "https://randomuser.me/api/portraits/med/women/88.jpg";
            pictures.large = "https://randomuser.me/api/portraits/thumb/women/88.jpg";


            Address address = new Address();
            address.city  = "warren";
            address.countryCode = "US";      
            address.postalCode  = "63450";
            address.state = "oregon";
            address.street = "6174 elgin st";

            Userprofile user = new Userprofile(
                username: username,
                Title: Title.Mrs,
                FirstName: "Aurora",
                LastName: "Smith",
                DateOfBirth: new DateTime(1983, 07, 12).Date,
                FavoriteGenres: favGenred,
                Email: "aurora@home.it",
                Phones: lstPhones,
                Picture: pictures,
                Address: address,
                Gender: Gender.F,
                Pwd: "123456",
                Status: Status.Active, 
                Type: "userprofile",
                Created: new DateTime(2022, 04, 01).Date,
                Updated: new DateTime(2022, 04, 01).Date
            );
            return user;
        }
        
        public Task InitializeAsync() {
            return Task.CompletedTask;
        }

        public Task DisposeAsync() {
            return Task.CompletedTask;
        }
    }
}