using Newtonsoft.Json;
using System;
using Microsoft.WindowsAzure.MobileServices;

namespace FrisbyBall.Models
{
    public class User
    {
        private string id;
        private string userName;
        private string password;
        private string email;
        private int matchCount;
        private int wins;
        private int loses;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value;}
        }

        [JsonProperty(PropertyName = "userName")]
        public string UserName
        {
            get { return userName; }
            set { userName = value;}
        }

        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return password; }
            set { password = value;}
        }

        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return email; }
            set { email = value;}
        }

        [JsonProperty(PropertyName = "MatchCount")]
        public int MatchCount
        {
            get { return matchCount; }
            set { matchCount = value;}
        }

        [JsonProperty(PropertyName = "wins")]
        public int Wins
        {
            get { return wins; }
            set { wins = value; }
        }

        [JsonProperty(PropertyName = "loses")]
        public int Loses
        {
            get { return loses; }
            set { loses = value;}
        }

        [Version]
        public string Version { get; set; }

    }
}
