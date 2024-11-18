using UnityEngine;

namespace Onion_AI
{
    public class LoadPlayer : MonoBehaviour
    {
        [SerializeField] private Jet_Data[] availableJetOptions;

        private void Awake()
        {
            //Store all Jet Data use Unity Addresables;
        }

        public void LoadSelectedJet()
        {
            int selectedCharacter = PlayerPrefs.GetInt("selectedOption");

            Jet_Data selectedJet = availableJetOptions[selectedCharacter];
            GameObject playerManager = Instantiate(selectedJet.jetObject, transform);
        }
    }
}
