using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class PausePanel : MonoBehaviour
    {
        UIManager uIManager;
        private bool hasBeenAdded;

        [Header("Scene Name")]
        [SerializeField] private string lobbyScene;

        [Header("Buttons")]
        [SerializeField] private Button exitButton;
        [SerializeField] private Button continueButton;

        private void OnEnable()
        {
            if(uIManager == null)
            {
                uIManager = GetComponentInParent<UIManager>();
            }
            uIManager.PauseButton.interactable = false;

            //Pause Game
            AddListeners();
            Time.timeScale = 0;
            GameManager.gameState = GameState.Paused;
        }

        private void OnDisable()
        {
            //Unpause Game
            Time.timeScale = 1;
            uIManager.CountdownTextUI.gameObject.SetActive(true);
            
            GameManager.gameState = GameState.Resume;
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
        }

        private void ExitGame()
        {
            gameObject.SetActive(false);
            UIEventsStaticClass.LoadNewScene(lobbyScene);
        }

        private void ContinueGame()
        {
            gameObject.SetActive(false);
        }
    }
}
