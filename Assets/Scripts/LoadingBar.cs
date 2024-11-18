using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Onion_AI
{
    public class LoadingBar : MonoBehaviour
    {
        public Slider loadingSlider;       // Reference to the Slider UI
        public TMP_Text loadingText;       // Reference to the TextMeshPro text UI
        public string sceneToLoad = "Lobby"; // Scene to load, in this case "Lobby"

        // Start is called before the first frame update
        void Start()
        {
            // Start the coroutine to load the scene
            StartCoroutine(LoadSceneAsync());
        }

        // Coroutine to load the scene asynchronously
        IEnumerator LoadSceneAsync()
        {
            // Start loading the scene
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

            // Prevent the scene from switching immediately when loading reaches 90%
            operation.allowSceneActivation = false;

            // While the scene is still loading
            while (!operation.isDone)
            {
                // Calculate progress value (operation.progress goes from 0 to 0.9)
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                // Update the slider and text
                loadingSlider.value = progress;
                loadingText.text = (progress * 100f).ToString("F0") + "%";

                // Check if the load is complete (progress == 0.9 means it's ready, but scene activation is still pending)
                if (operation.progress >= 0.9f)
                {
                    // Update the slider to 100%
                    loadingSlider.value = 100f;
                    loadingText.text = "100%";

                    // Wait for a short moment to simulate some finalization time
                    yield return new WaitForSeconds(1f);

                    // Activate the scene
                    operation.allowSceneActivation = true;
                }

                // Wait until the next frame
                yield return null;
            }
        }
    }
}