using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Required for Button

namespace Onion_AI
{
    public class FriendsPanel : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text textToCopy;    // Text to be copied
        public TMP_Text popupText;     // Text for the popup that shows "Copied!"
        public GameObject popupObject; // The GameObject containing the popup text (optional, can be same as popupText)
        public Button copyButton;      // Button to trigger the copy action

        private bool isPopupActive = false;

        void Start()
        {
            // Initially hide the popup object if it's assigned
            if (popupObject != null)
            {
                popupObject.SetActive(false);
            }
            else
            {
                popupText.gameObject.SetActive(false);
            }

            // Add the OnClick listener to the button
            copyButton.onClick.AddListener(OnCopyButtonClick);
        }

        // This method is called when the button is clicked
        void OnCopyButtonClick()
        {
            CopyTextToClipboard();     // Copy the text
            ShowPopup();               // Show the "Copied!" popup
        }

        // Method to copy text to the system clipboard
        void CopyTextToClipboard()
        {
            GUIUtility.systemCopyBuffer = textToCopy.text; // Copies the TMP_Text to clipboard
        }

        // Method to show the popup for 5 seconds
        void ShowPopup()
        {
            if (!isPopupActive)
            {
                isPopupActive = true;

                // Show the popup text and set it to "Copied!"
                popupText.text = "Copied!";

                // Show the popup object if assigned, otherwise just the text
                if (popupObject != null)
                {
                    popupObject.SetActive(true);
                }
                else
                {
                    popupText.gameObject.SetActive(true);
                }

                // Start the coroutine to disable the popup after 5 seconds
                StartCoroutine(HidePopupAfterDelay(5f));
            }
        }

        // Coroutine to hide the popup after a delay
        IEnumerator HidePopupAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            // Hide the popup object or the text
            if (popupObject != null)
            {
                popupObject.SetActive(false);
            }
            else
            {
                popupText.gameObject.SetActive(false);
            }

            isPopupActive = false;
        }

        private void OnDestroy()
        {
            // Clean up the event listener when the object is destroyed
            copyButton.onClick.RemoveListener(OnCopyButtonClick);
        }
    }
}
