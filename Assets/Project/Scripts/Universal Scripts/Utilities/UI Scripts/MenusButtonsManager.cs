using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Onion_AI
{
    public class MenusButtonsManager : MonoBehaviour
    {
        private bool hasBeenInitialized;
        private MenuButton[] menuButtons;
        public List<GameObject> Panels {get; private set;} = new();

        [Header("Buttons")]
        public Button playButton;

        private void Awake()
        {
            menuButtons = FindObjectsOfType<MenuButton>();
        }

        private void OnEnable()
        {
            InitializeMenuButtons();
        }

        private void OnDisable()
        {
            hasBeenInitialized = false;
        }

        private void InitializeMenuButtons()
        {
            if(hasBeenInitialized)
            {
                return;
            }

            foreach(var menuButton in menuButtons)
            {
                if(Panels.Contains(menuButton.menuToDisplay) != true)
                {
                    Panels.Add(menuButton.menuToDisplay);
                }
                menuButton.Initialize(this);
            }
            
            hasBeenInitialized = true;
            UIEventsStaticClass.AddButtonListener(playButton, PlayGame);
        }

        public void PlayGame()
        {
            //Change Load Scene to Load Scene Based On Level Selected
            if(HealthCounterManager.Instance.currentLifeCount <= 0)
            {
                HealthCounterManager.Instance.currentLifeCount = 0;
                return;
            }
            SceneManager.LoadScene("Game Scene", LoadSceneMode.Single);
        }
    }
}
