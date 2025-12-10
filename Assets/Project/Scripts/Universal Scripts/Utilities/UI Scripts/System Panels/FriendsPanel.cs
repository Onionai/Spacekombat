using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Onion_AI
{
    public class FriendsPanel : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text textToCopy;
        public TMP_Text popupText;
        public GameObject popupObject;
        public Button copyButton;

        private bool isPopupActive = false;

        void Start()
        {
            if (popupObject != null)
            {
                popupObject.SetActive(false);
            }
            else
            {
                popupText.gameObject.SetActive(false);
            }
            copyButton.onClick.AddListener(OnCopyButtonClick);
        }

        void OnCopyButtonClick()
        {
            CopyTextToClipboard();
            ShowPopup();
        }

        void CopyTextToClipboard()
        {
            GUIUtility.systemCopyBuffer = textToCopy.text;
        }

        void ShowPopup()
        {
            if (!isPopupActive)
            {
                isPopupActive = true;
                popupText.text = "Copied!";

                if (popupObject != null)
                {
                    popupObject.SetActive(true);
                }
                else
                {
                    popupText.gameObject.SetActive(true);
                }
                StartCoroutine(HidePopupAfterDelay(5f));
            }
        }

        IEnumerator HidePopupAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

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
            copyButton.onClick.RemoveListener(OnCopyButtonClick);
        }
    }
}
