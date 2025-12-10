using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    [System.Serializable]
    public class UserProfile
    {
        [field: Header("Player Information")]
        public string UserID {get; private set;}
        public string UserName {get; private set;}
        public Sprite ProfilePicture {get; private set;}

        [field: Header("Player Game Status")]
        public int HighScore { get; private set;}
        public int TotalScore {get; private set;}
        public int CoinsCollected {get; private set;}
        public List<Task> TaskList {get; private set;}
        public List<Jet_Data> JetsOwned {get; private set;} = new();

        public UserProfile(string UserID, string UserName)
        {
            this.UserID = UserID;
            this.UserName = UserName;

            TotalScore = 0;
            CoinsCollected = 0;
            TaskList = new List<Task>();
            JetsOwned = new List<Jet_Data>();
        }

        public void AddSessionCoinAndScore(int sessionScore, int sessionCoin)
        {
            TotalScore += sessionScore;
            CoinsCollected += sessionCoin;
        }
    }
}