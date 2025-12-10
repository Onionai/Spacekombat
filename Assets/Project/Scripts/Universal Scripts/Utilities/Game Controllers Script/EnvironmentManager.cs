using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class EnvironmentManager : MonoBehaviour
    {
        public static float gameSpeedMultiplier = 1f;
        [SerializeField] private float environmentSpeedMultiplier;

        [Header("Images")]
        [SerializeField] private RawImage mainBackGround;

        [Header("Environment Parameters")]
        [SerializeField] private Vector2 scrollVector;

        public void EnvironmentManager_Updater(float delta)
        {
            BackgroundScroller(delta);
        }

        private void BackgroundScroller(float delta)
        {
            mainBackGround.uvRect = 
                new(mainBackGround.uvRect.position + delta * environmentSpeedMultiplier * scrollVector, mainBackGround.uvRect.size);         
        }
    }
}
