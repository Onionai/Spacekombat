using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class TaskPanel : MonoBehaviour
    {
        UIManager uIManager;
        private bool hasBeenAdded;
        public static TaskPanel Instance {get; private set;}
        private List<TaskItemUI> taskItemUIList = new List<TaskItemUI>();

        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject exitPanel;
        [SerializeField] public GameObject friendPanel;
        [SerializeField] private GameObject taskItemPrefab;
        [SerializeField] private Transform taskItemListParent;

        [SerializeField] private Image completedLogo;
        [SerializeField] private Image incompleteLogo;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            if(uIManager == null)
            {
                uIManager = GetComponentInParent<UIManager>();
            }

            if(uIManager != null)
            {
                uIManager.PauseButton.interactable = false;
            }

            AddListeners();
            Time.timeScale = 0;
            GameManager.gameState = GameState.Paused;
        }

        private void OnDisable()
        {
            if(uIManager != null) uIManager.PauseButton.interactable = true;
        }

        private void AddListeners()
        {
            if(hasBeenAdded)
            {
                return;
            }

            hasBeenAdded = true;
            UIEventsStaticClass.AddButtonListener(exitButton, ExitGame);
        }

        public void RefreshTaskPanel(List<Task> taskList)
        {
            if(taskItemUIList.Count >= taskList.Count)
            {
                return;
            }

            foreach(Task task in taskList)
            {
                GameObject taskItem = Instantiate(taskItemPrefab, taskItemListParent);

                TaskItemUI itemUI = taskItem.GetComponent<TaskItemUI>();
                itemUI.SetTask(task);
                taskItemUIList.Add(itemUI);
            }
        }

        private void ExitGame()
        {
            gameObject.SetActive(false);

            exitPanel.SetActive(true);
        }
    }
}
