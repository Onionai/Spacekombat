using TMPro;
using UnityEngine;

namespace Onion_AI
{
    public class DisableAllSound : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown soundDropdown;  // Reference to the TMPro Dropdown

        // Start is called before the first frame update
        void Start()
        {
            // Load the saved sound setting (0 = Sound Off, 1 = Sound On), default to 1 (On)
            int savedSoundSetting = PlayerPrefs.GetInt("SoundSetting", 1);

            // Set the dropdown value based on saved preference
            soundDropdown.value = savedSoundSetting;

            // Apply the sound setting immediately
            ApplySoundSetting(savedSoundSetting);

            // Listen for changes in the dropdown
            soundDropdown.onValueChanged.AddListener(delegate {
                OnDropdownValueChanged(soundDropdown);
            });
        }

        // Called when the dropdown value changes
        void OnDropdownValueChanged(TMP_Dropdown dropdown)
        {
            // Get the selected option (0 for Off, 1 for On)
            int selectedOption = dropdown.value;

            // Apply the sound setting
            ApplySoundSetting(selectedOption);

            // Save the sound setting to persist across scenes/sessions
            PlayerPrefs.SetInt("SoundSetting", selectedOption);
        }

        // Applies the sound setting: 0 = Disable sound, 1 = Enable sound
        void ApplySoundSetting(int soundSetting)
        {
            // If soundSetting is 0 (Sound Off), disable AudioListener
            if (soundSetting == 0)
            {
                AudioListener.pause = true;
                Debug.Log("Sound saved: Off");
            }
            // If soundSetting is 1 (Sound On), enable AudioListener
            else
            {
                AudioListener.pause = false;
                Debug.Log("Sound saved: On");
            }
        }
    }

}

