using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebAPI {
    [Serializable]
    public class Username {
        public string GetKey(long id) => $"userprofile-{username}";

        [JsonProperty("username")]
        public string username {get; set;}

        [JsonProperty("title")]
        public string title {get; set;}

        [JsonProperty("firstName")]
        public string firstName {get; set;}

        [JsonProperty("lastName")]
        public string lastName {get; set;}

        [JsonProperty("dateOfBirth")]
        public DateTime dateOfBirth {get; set;}

        [JsonProperty("favoriteGenres")]
        public List<string> favoriteGenres {get; set;}

        [JsonProperty("email")]
        public string email {get; set;}
        
        [JsonProperty("phones")]
        public Phones phones {get; set;}

        [JsonProperty("picture")]
        public Pictures picture {get; set;}

        [JsonProperty("address")]
        public Address address {get; set;}
        
        [JsonProperty("gender")]
        public string gender {get; set;}

        [JsonProperty("pwd")]
        public string pwd {get; set;}

        [JsonProperty("status")]
        public string status {get; set;}

        [JsonProperty("type")]
        public string type {get; set;}

        [JsonProperty("created")]
        public DateTime created {get; set;}

        [JsonProperty("updated")]
        public DateTime updated {get; set;}

        public Username(string username, String Title, string FirstName, string LastName, 
                        DateTime DateOfBirth, List<string> FavoriteGenres, string Email, Phones Phones,
                        Pictures Picture, Address Address, string Gender, string Pwd, 
                        string Status, string Type, DateTime Created, DateTime Updated) {
            this.username = username;
            this.title = Title;
            this.firstName = FirstName;
            this.lastName = LastName;
            this.dateOfBirth = DateOfBirth;
            this.favoriteGenres = FavoriteGenres;
            this.email = Email;
            this.phones = Phones;
            this.picture = Picture;
            this.address = Address;
            this.gender = Gender;
            this.pwd = Pwd;
            this.status = Status;
            this.type = Type;
            this.created = Created;
            this.updated = Updated;       
        }
    }
}