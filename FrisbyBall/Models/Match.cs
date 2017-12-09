using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace FrisbyBall.Models
{
    public class Match
    {
        private string id;
        private string playerWon;
        private string playerLost;
        private int winPoints;
        private int lostPoints;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value;}
        }

        [JsonProperty(PropertyName = "PlayerWon")]
        public string PlayerWon
        {
            get { return playerWon; }
            set { playerWon = value;}
        }

        [JsonProperty(PropertyName = "PlayerLost")]
        public string PlayerLost
        {
            get { return playerLost; }
            set { playerLost = value;}
        }

        [JsonProperty(PropertyName = "WinPoints")]
        public int WinPoints
        {
            get { return winPoints; }
            set { winPoints = value;}
        }

        [JsonProperty(PropertyName = "LostPoints")]
        public int LostPoints
        {
            get { return lostPoints; }
            set { lostPoints = value;}
        }

        [Version]
        public string Version { get; set; }
    }
}
