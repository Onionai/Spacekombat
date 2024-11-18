using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Onion_AI
{
    public static class UIEventsStaticClass
    {
        public static void AddButtonListener(Button button, System.Action function)
        {
            button.onClick.AddListener(() => function());
        }

        public static void LoadNewScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}