using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class Tutorial : MonoBehaviour
    {
        public GameObject panel;
        public Button destroyButton;
        private readonly string panelDestroyedKey = "PanelDestroyed";

        void Start()
        {
            if (PlayerPrefs.GetInt(panelDestroyedKey, 0) == 1)
            {
                panel.SetActive(false);
            }
            destroyButton.onClick.AddListener(DestroyPanel);
        }

        void DestroyPanel()
        {
            if (panel != null)
            {
                panel.SetActive(false);

                PlayerPrefs.SetInt(panelDestroyedKey, 1);
                PlayerPrefs.Save();
            }
        }
    }
}