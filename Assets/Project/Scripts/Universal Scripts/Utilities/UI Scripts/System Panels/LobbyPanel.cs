using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class LobbyPanel : MonoBehaviour
    {
        [Header("Selection Menu Buttons")]
        [SerializeField] private Button nextButton;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button selectButton;

        [Header("Selected Character Properties")]
        [SerializeField] private RawImage displayedCharacter;
        [SerializeField] private TextMeshProUGUI displayedCharacterName;

        [Header("Selected Options")]
        private int selectedOption;
        [SerializeField] private GameObject tonButton;
        [SerializeField] private Jet_Data[] availableJetOptions;
        [SerializeField] private Jet_Data currentlySelectedOption;

        private void OnEnable()
        {
            if(tonButton.activeSelf != true)
            {
                tonButton.SetActive(true);
            }
        }

        private void Start()
        {
            selectedOption = 0;
            LoadCharacter();

            UIEventsStaticClass.AddButtonListener(nextButton, NextCharacter);
            UIEventsStaticClass.AddButtonListener(prevButton, PrevCharacter);
            UIEventsStaticClass.AddButtonListener(selectButton, SelectCharacter);
        }

        private void OnDisable()
        {
            if(tonButton != null)
            {
                tonButton.SetActive(false);
            }
        }

        #region Selection Functionalities

        private void NextCharacter()
        {
            selectedOption++;
            if(selectedOption >= availableJetOptions.Length)
            {
                selectedOption = 0;
            }
            LoadCharacter();
        }

        private void PrevCharacter()
        {
            selectedOption--;
            if(selectedOption < 0)
            {
                selectedOption += availableJetOptions.Length;
            }
            LoadCharacter();
        }

        private void LoadCharacter()
        {
            currentlySelectedOption = availableJetOptions[selectedOption];

            displayedCharacter.texture = currentlySelectedOption.jetImage;
            displayedCharacterName.text = currentlySelectedOption.jetName;
        }

        private void SelectCharacter()
        {
            PlayerPrefs.SetInt("selectedOption", selectedOption);
        }

        #endregion

        #region Menu Selection Functionalities



        #endregion
    }
}
