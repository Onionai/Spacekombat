using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class EnvironmentManager : MonoBehaviour
    {
        private float elapsedTime = 0f;
        public static float gameSpeedMultiplier = 1f;

        [Header("Game Tempo")]
        public float speedIncreaseRate = 0.1f;
        private float nextIncreaseTime = 0.0f;
        public float timeBetweenIncreases = 20.0f; //In Seconds

        [Header("Environment Parameters")]
        [SerializeField] private RawImage rawImage;
        [SerializeField] private Vector2 scrollVector;

        private void Awake()
        {
            rawImage = GetComponent<RawImage>();
        }

        private void Start()
        {
            nextIncreaseTime = Time.time +  timeBetweenIncreases;
        }

        public void EnvironmentManager_Updater(float delta)
        {
            BackgroundScroller(delta);
            IncreaseGameTempoPeriodically(delta);
        }

        private void IncreaseGameTempoPeriodically(float delta)
        {
            elapsedTime += delta;

            if(Time.time >= nextIncreaseTime)
            {
                gameSpeedMultiplier += speedIncreaseRate;
                nextIncreaseTime = Time.time + timeBetweenIncreases;
            }
        }

        private void BackgroundScroller(float delta)
        {
            rawImage.uvRect = new Rect(rawImage.uvRect.position + scrollVector * gameSpeedMultiplier * delta, rawImage.uvRect.size);
        }
    }
}
