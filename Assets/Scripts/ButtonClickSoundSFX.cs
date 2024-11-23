using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required to access UI elements like Button

namespace Onion_AI
{
    public class ButtonClickSoundSFX : MonoBehaviour
    {
        public AudioSource ClickSound; // The click sound effect

        void Update()
        {
            // Find all buttons in the scene
            Button[] buttons = FindObjectsOfType<Button>();

            // Loop through all buttons
            foreach (Button button in buttons)
            {
                // Add the PlaySound method to the button's OnClick event
                button.onClick.AddListener(PlaySound);
            }
        }

        // This method plays the click sound
        public void PlaySound()
        {
            if (ClickSound != null)
            {
                ClickSound.Play(); // Play sound when clicked
            }
        }
    }
}
