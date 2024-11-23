using UnityEngine;

namespace Onion_AI
{
    [CreateAssetMenu(fileName = "Jet_Data", menuName = "OnionAI/JetData")]
    public class Jet_Data : ScriptableObject
    {
        public string jetName;
        public Texture jetImage;
        public GameObject jetObject;
        public JetStatistics jetStatistics;
        //Bio
    }
}
