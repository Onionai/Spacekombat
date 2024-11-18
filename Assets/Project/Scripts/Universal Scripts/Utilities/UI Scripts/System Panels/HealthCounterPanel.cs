using TMPro;
using System;
using UnityEngine;

namespace Onion_AI
{
    public class HealthCounterPanel : MonoBehaviour
    {
        public bool lobbyScene;
        [field: Header("Health UI")]
        public TimeSpan timeSinceDamage;
        public GameObject[] healthBarArray;
        public TextMeshProUGUI healthTimer;

        private void Awake()
        {
            if(lobbyScene != true)
            {
                return;
            }
            HealthCounterManager.Instance.SetHealthCounterPanel(this);
        }

        public void UpdateHealthUI(TimeSpan timeSpan)
        {
            print(7);            
            for(int i = 0; i < healthBarArray.Length; i++)
            {
                bool shouldHideBar = i < HealthCounterManager.Instance.currentLifeCount;
                healthBarArray[i].SetActive(shouldHideBar);
            }
            healthTimer.text = string.Format("{0:00} : {1:00} : {2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
