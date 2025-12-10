using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class ExitPanel : MonoBehaviour
    {
        UIManager uIManager;
        private bool hasBeenAdded;

        [Header("Scene Name")]
        [SerializeField] private string lobbyScene;

        [Header("Buttons")]
        [SerializeField] private Button exitButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button tasksButton;

        [Header("Task Panel")]
        [SerializeField] private GameObject taskPanel;
        [SerializeField] private TextMeshProUGUI totalScoreUI;
        [SerializeField] private TextMeshProUGUI totalCoinsUI;

        private void Awake()
        {
            uIManager = GetComponentInParent<UIManager>();
        }

        private void OnEnable()
        {
            uIManager.PauseButton.interactable = false;

            GameManager gameManager = GameManager.Instance;
            //totalScoreUI.text = "Total Score: " + gameManager.totalScore;
            totalCoinsUI.text = "Total Coins: " + gameManager.playerManager.coinCount;

            AddListeners();
            Time.timeScale = 0;
            GameManager.Instance.Controller.SwitchGameState(GamePlayState.PlayerPause);
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
            uIManager.PauseButton.interactable = true;
            GameManager.Instance.Controller.SwitchGameState(GamePlayState.Active);
        }

        private void AddListeners()
        {
            if(hasBeenAdded)
            {
                return;
            }

            hasBeenAdded = true;
            UIEventsStaticClass.AddButtonListener(exitButton, ExitGame);
            UIEventsStaticClass.AddButtonListener(retryButton, StartNewGame);
            UIEventsStaticClass.AddButtonListener(tasksButton, DisplayTasksPanel);
        }

        private void ExitGame()
        {
            gameObject.SetActive(false);
            UIEventsStaticClass.LoadNewScene(lobbyScene);
        }

        public void StartNewGame()
        {
            if(TriesCounterManager.Instance.CurrentTriesNumber <= 0)
            {
                return;
            }
            gameObject.SetActive(false);
            UIEventsStaticClass.LoadNewScene("Game Scene");
        }

        private void DisplayTasksPanel()
        {
            gameObject.SetActive(false);
            taskPanel.SetActive(true);
        }
    }
}
