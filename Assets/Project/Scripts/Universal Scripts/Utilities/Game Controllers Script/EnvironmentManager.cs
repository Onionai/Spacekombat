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
        [SerializeField] private RawImage planetBackGround;

        [Header("Environment Parameters")]
        [SerializeField] private Vector2 scrollVector;

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        public void EnvironmentManager_Updater(float delta)
        {
            BackgroundScroller(delta);
        }

        private void BackgroundScroller(float delta)
        {
            mainBackGround.uvRect = new Rect(mainBackGround.uvRect.position + scrollVector * environmentSpeedMultiplier * delta, mainBackGround.uvRect.size);
            planetBackGround.uvRect = new Rect(planetBackGround.uvRect.position + scrollVector * 2 * environmentSpeedMultiplier * delta, mainBackGround.uvRect.size);
        }
    }
}
