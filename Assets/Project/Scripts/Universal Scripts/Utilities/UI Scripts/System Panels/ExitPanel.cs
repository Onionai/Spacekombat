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

        private void OnEnable()
        {
            if(uIManager == null)
            {
                uIManager = GetComponentInParent<UIManager>();
                totalScoreUI.text = "Total Score: " + GameManager.totalScore;
            }
            
            uIManager.PauseButton.interactable = false;

            AddListeners();
            Time.timeScale = 0;
            GameManager.gameState = GameState.Paused;
        }

        private void OnDisable()
        {
            //Unpause Game
            Time.timeScale = 1;
            uIManager.PauseButton.interactable = true;
            GameManager.gameState = GameState.Active;
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
            if(HealthCounterManager.Instance.currentLifeCount <= 0)
            {
                HealthCounterManager.Instance.currentLifeCount = 0;
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
