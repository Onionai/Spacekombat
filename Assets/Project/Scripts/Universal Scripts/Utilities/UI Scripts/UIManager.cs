using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Onion_AI
{
    public class UIManager : MonoBehaviour
    {
        bool hasDisplayed;
        public GameManager gameManager;
        private WaitForSeconds waitForSeconds;

        [Header("UI Text")]
        [SerializeField] private TextMeshProUGUI coinCountUI;
        [SerializeField] private TextMeshProUGUI highScoreUI;
        [SerializeField] private TextMeshProUGUI roundCountUI;

        private void Awake()
        {
            waitForSeconds = new WaitForSeconds(3.0f);
        }

        public void DisplayTotalScore(int totalScore)
        {
            highScoreUI.text = "SCORE: " + totalScore.ToString("D9");
        }

        public void DisplayCoinCount(int coinCount)
        {
            coinCountUI.text = "X " + coinCount.ToString("D3");
        }

        public void SetRoundCount(int roundCount)
        {
            if(gameManager.canProceed == true)
            {
                return;
            }

            gameManager.canProceed = false;

            roundCountUI.text = "ROUND: " + roundCount;
            roundCountUI.gameObject.SetActive(true);
            StartCoroutine(DisableRoundCount());
        }

        private IEnumerator DisableRoundCount()
        {
            yield return waitForSeconds;
            if(roundCountUI.gameObject.activeSelf)
            {
                roundCountUI.gameObject.SetActive(false);
            }
            gameManager.canProceed = true;
        }
    }
}
