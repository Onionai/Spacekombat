using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class EnvironmentManager : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;
        [SerializeField] private Vector2 scrollVector;

        private void Awake()
        {
            rawImage = GetComponent<RawImage>();
        }

        public void EnvironmentManager_Updater(float delta)
        {
            BackgroundScroller(delta);
        }

        private void BackgroundScroller(float delta)
        {
            rawImage.uvRect = new Rect(rawImage.uvRect.position + scrollVector * delta, rawImage.uvRect.size);
        }
    }
}
