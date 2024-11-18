using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class UIManager : MonoBehaviour
    {
        private Camera mainCamera;
        private Canvas[] canvasArray;
        public GameManager gameManager;
        private Coroutine countdownCoroutine;
        private WaitForSeconds waitForSeconds;

        [field: Header("Health Bar")]
        [field: SerializeField] public UIBar PlayerHealthBar {get; private set;}

        [Header("Pause And Panels")]
        [SerializeField] private ExitPanel exitMenu;
        [SerializeField] private PausePanel pauseMenu;
        [field: SerializeField] public Button PauseButton {get; private set;}

        [Header("UI Text")]
        [SerializeField] private TextMeshProUGUI coinCountUI;
        [SerializeField] private TextMeshProUGUI topRightRoundCountUI;
        [field: SerializeField] public TextMeshProUGUI CountdownTextUI {get; private set;}
        [field: SerializeField] public TextMeshProUGUI CenterRoundCountUI {get; private set;}

        private void Awake()
        {
            mainCamera = Camera.main;
            waitForSeconds = new WaitForSeconds(1.0f);
            canvasArray = GetComponentsInChildren<Canvas>();

            foreach(var canvas in canvasArray)
            {
                canvas.worldCamera = mainCamera;
            }
        }

        private void Start()
        {
            UIEventsStaticClass.AddButtonListener(PauseButton, DisplayPauseMenu);
        }

        public void HandlePauseCountdown()
        {
            if(countdownCoroutine == null)
            {
                countdownCoroutine = StartCoroutine(PauseCountDelay(3));
            }
        }

        public void DisplayCoinCount(int coinCount)
        {
            coinCountUI.text = "X " + coinCount.ToString("D7");
        }

        public void DisplayExitMenu(bool status)
        {
            exitMenu.gameObject.SetActive(status);
        }

        private void DisplayPauseMenu()
        {
            PauseButton.interactable = false;
            pauseMenu.gameObject.SetActive(true);
        }

        public void SetRoundCount(int roundCount)
        {
            CenterRoundCountUI.text = "WAVE: " + roundCount;
            topRightRoundCountUI.text = "WAVE: " + roundCount;
            
            CenterRoundCountUI.gameObject.SetActive(true);
        }

        public IEnumerator PauseCountDelay(int countdown)
        {
            while(countdown > 0)
            {
                CountdownTextUI.text = countdown.ToString();

                yield return waitForSeconds;

                countdown--;
            }

            CountdownTextUI.text = "GO!";

            yield return waitForSeconds;
            PauseButton.interactable = true;
            GameManager.gameState = GameState.Active;
            CountdownTextUI.gameObject.SetActive(false);

            yield return new WaitUntil(() => CountdownTextUI.gameObject.activeSelf == false);
            countdownCoroutine = null;
        }
    }
}
