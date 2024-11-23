using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace Onion_AI
{
    [System.Serializable]
    public class UserProfile
    {
        const string digits = "0123456789";
        const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
        const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string specialCharacters = "!@#$%^&*()_+-=|:;<>,.?/";

        [field: Header("Player Information")]
        public string UserID {get; private set;}
        public string UserName {get; private set;}
        public byte[] UserPassword {get; private set;}
        public Sprite ProfilePicture {get; private set;}

        [field: Header("Player Game Status")]
        public int TotalScore {get; private set;}
        public int CoinsCollected {get; private set;}
        public List<Task> taskList {get; private set;}
        public List<Jet_Data> JetsOwned {get; private set;} = new();

        [field: Header("Wallet and Finance Details")]
        public char TelegramWalletAddress {get; private set;}

        public UserProfile(string UserID, string UserName)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            UserPassword = GeneratePassword();

            TotalScore = 0;
            CoinsCollected = 0;
            taskList = new List<Task>();
            JetsOwned = new List<Jet_Data>();
        }

        public void AddSessionCoinAndScore(int sessionScore, int sessionCoin)
        {
            TotalScore += sessionScore;
            CoinsCollected += sessionCoin;
        }

        private byte[] GeneratePassword()
        {
            var random = new System.Random();
            var password = new StringBuilder();

            password.Append(digits[random.Next(digits.Length)]);
            password.Append(upperCase[random.Next(upperCase.Length)]);
            password.Append(lowerCase[random.Next(lowerCase.Length)]);
            password.Append(specialCharacters[random.Next(specialCharacters.Length)]);

            int passwordLength = Random.Range(8,17);
            string allChars = upperCase + lowerCase + digits + specialCharacters;
            for(int i = 4; i < passwordLength; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }
            return Encoding.UTF8.GetBytes(password.ToString());
        }
    }
}