using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class MenuButton : MonoBehaviour
    {
        public Button button;
        public GameObject menuToDisplay;
        private MenusButtonsManager menusButtonsManager;

        public void Initialize(MenusButtonsManager mbm)
        {
            menusButtonsManager = mbm;

            button = GetComponent<Button>();
            UIEventsStaticClass.AddButtonListener(button, SetMenuActive);
        }

        private void SetMenuActive()
        {
            foreach(GameObject panel in menusButtonsManager.Panels)
            {
                if(panel == null)
                {
                    continue;
                }
                panel.SetActive(panel == menuToDisplay);
            }
        }
    }
}