using TMPro; 
using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class GraphicSettings : MonoBehaviour
    {
        public TMP_Dropdown qualityDropdown; // Reference to the dropdown in the Inspector
        private const string QualityPrefKey = "QualitySetting"; // Key for saving quality settings

        // Start is called before the first frame update
        void Start()
        {
            // Populate the dropdown with quality settings
            qualityDropdown.ClearOptions();
            List<string> options = new List<string>(QualitySettings.names);
            qualityDropdown.AddOptions(options);

            // Load the saved quality level from PlayerPrefs
            int savedQualityLevel = PlayerPrefs.GetInt(QualityPrefKey, QualitySettings.GetQualityLevel());

            // Apply the saved quality level
            QualitySettings.SetQualityLevel(savedQualityLevel);

            // Set the dropdown to the saved value
            qualityDropdown.value = savedQualityLevel;
            qualityDropdown.RefreshShownValue();

            // Add listener to handle value changes
            qualityDropdown.onValueChanged.AddListener(delegate { ChangeQuality(qualityDropdown.value); });
        }

        // Method to change quality based on dropdown selection
        public void ChangeQuality(int index)
        {
            // Set the selected quality level
            QualitySettings.SetQualityLevel(index);

            // Save the selected quality level to PlayerPrefs
            PlayerPrefs.SetInt(QualityPrefKey, index);
            PlayerPrefs.Save(); // Ensure that the data is written to disk

            Debug.Log("Quality changed to: " + QualitySettings.names[index] + " and saved.");
        }
    }

}