using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Onion_AI
{
    public class PauseController : MonoBehaviour
    {
        UIManager uiManager;
        private bool hasBeenAdded;
        private WaitForSeconds waitForSeconds;
        private GameplayController gameplayController;

        [Header("Parameters")]
        [SerializeField] private string lobbyScene;
        [SerializeField] private GameObject pausePanel;

        [Header("Buttons")]
        [SerializeField] private Button exitButton;
        [SerializeField] private Button continueButton;

        private void Awake()
        {
            waitForSeconds = new(1.0f);
            uiManager = GetComponentInParent<UIManager>();
        }

        private void OnEnable()
        {
            pausePanel.SetActive(true);
            gameplayController ??= GameManager.Instance.Controller;
            uiManager.PauseButton.interactable = false;

            AddListeners();
            Time.timeScale = 0;
            gameplayController.SwitchGameState(GamePlayState.PlayerPause);
        }

        private void OnDisable()
        {
            gameplayController.SwitchGameState(GamePlayState.Active);
        }

        private void AddListeners()
        {
            if(hasBeenAdded)
            {
                return;
            }

            hasBeenAdded = true;
            UIEventsStaticClass.AddButtonListener(exitButton, ExitGame);
            UIEventsStaticClass.AddButtonListener(continueButton, ContinueGame);
            gameplayController.AddStateListener(HandlePauseCountdown);
        }

        private void ExitGame()
        {
            gameObject.SetActive(false);
            UIEventsStaticClass.LoadNewScene(lobbyScene);
        }

        private void ContinueGame()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            gameplayController.SwitchGameState(GamePlayState.PlayerResume);
        }

        private void HandlePauseCountdown(GamePlayState gamePlayState)
        {
            print(5);
            if(gamePlayState == GamePlayState.PlayerResume)
            {
                print(10);
                StartCoroutine(PauseCountDelay(3));
            }
        }

        private IEnumerator PauseCountDelay(int countdown)
        {
            uiManager.CenterTextUI.gameObject.SetActive(true);
            TextMeshProUGUI countDownUI = uiManager.CenterTextUI;
            while (countdown > 0)
            {
                //countDownUI.gameObject.SetActive(true);
                countDownUI.text = countdown.ToString();
                yield return waitForSeconds;
                countdown--;
            }
            countDownUI.text = "GO!";

            yield return waitForSeconds;
            countDownUI.gameObject.SetActive(false);
            uiManager.PauseButton.interactable = true;

            yield return new WaitUntil(() => countDownUI.gameObject.activeSelf == false);
            gameObject.SetActive(false);
        }
    }
}
