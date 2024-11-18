using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include the UI namespace
using TMPro; //Stuff for visual studio code

namespace Onion_AI
{
    public class Tutorial : MonoBehaviour
    {
        public GameObject panel; // Reference to the panel GameObject
        public Button destroyButton; // Reference to the button GameObject

        private string panelDestroyedKey = "PanelDestroyed"; // Key to store in PlayerPrefs

        // Start is called before the first frame update
        void Start()
        {
            // Check if the panel is already destroyed (saved in PlayerPrefs)
            if (PlayerPrefs.GetInt(panelDestroyedKey, 0) == 1)
            {
                // If panel was destroyed before, disable it
                panel.SetActive(false);
            }

            // Add a listener to the button to call the DestroyPanel method when clicked
            destroyButton.onClick.AddListener(DestroyPanel);
        }

        // Method to destroy the panel and save the state in PlayerPrefs
        void DestroyPanel()
        {
            if (panel != null)
            {
                // Destroy or deactivate the panel
                panel.SetActive(false);

                // Save the destroyed state in PlayerPrefs (1 means destroyed)
                PlayerPrefs.SetInt(panelDestroyedKey, 1);
                PlayerPrefs.Save(); // Make sure to save the preferences
            }
        }
    }
}