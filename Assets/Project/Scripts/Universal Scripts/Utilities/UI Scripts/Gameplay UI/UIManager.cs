using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class UIManager : MonoBehaviour
    {
        private Camera mainCamera;
        private Canvas[] canvasArray;

        [field: Header("Health Bar")]
        [field: SerializeField] public UIBar PlayerHealthBar {get; private set;}

        [Header("Pause And Panels")]
        [SerializeField] private ExitPanel exitMenu;
        [SerializeField] private PauseController pauseMenu;
        [field: SerializeField] public Button PauseButton {get; private set;}

        [Header("UI Text")]
        [SerializeField] private TextMeshProUGUI coinCountUI;
        [SerializeField] private TextMeshProUGUI topRightRoundCountUI;
        [field: SerializeField] public TextMeshProUGUI CenterTextUI {get; private set;}

        private void Awake()
        {
            mainCamera = Camera.main;
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
            SetCenterText("WAVE: " + roundCount);
            topRightRoundCountUI.text = "WAVE: " + roundCount;
        }

        public void SetCenterText(string text)
        {
            CenterTextUI.text = text;
            CenterTextUI.gameObject.SetActive(true);
        }
    }
}
