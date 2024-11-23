using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Leaderboards.Models;

namespace Onion_AI
{
    public class LeaderboardItem : MonoBehaviour
    {
        private LeaderboardEntry playerEntry = null;

        [Header("User Profile")]
        public GameObject playerProfile;
        public Button userProfileButton;

        [Header("Player Information")]
        [SerializeField] private TextMeshProUGUI playerRankText;
        [SerializeField] private TextMeshProUGUI playerUserNameText;
        [SerializeField] private TextMeshProUGUI playerTotalScoreText;

        private void Start()
        {
            UIEventsStaticClass.AddButtonListener(userProfileButton, ShowProfile);
        }

        public void Initialize(LeaderboardEntry player)
        {
            playerEntry = player;

            playerUserNameText.text = playerEntry.PlayerName;
            playerRankText.text = (playerEntry.Rank + 1).ToString();
            playerTotalScoreText.text = playerEntry.Score.ToString();
        }

        private void ShowProfile()
        {
            playerProfile.SetActive(true);
            //Set Information and Disable Leaderboard Screen;
        }
    }
}
