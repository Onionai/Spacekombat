using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class TaskItemUI : MonoBehaviour
    {
        bool hasBeenSet;
        private Task task;

        private string socialsURL;
        private TaskType taskType;

        [Header("Parameters")]
        [SerializeField] private Image rewardLogo;
        [SerializeField] private Image taskStatus;
        [SerializeField] private Button taskButton;
        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private TextMeshProUGUI descriptionText;

        private void OnEnable()
        {
            if(task != null)
            {
                taskStatus.gameObject.SetActive(task.isCompleted != true);
            }
            
            if(hasBeenSet)
            {
                return;
            }
            hasBeenSet = true;
            UIEventsStaticClass.AddButtonListener(taskButton, TaskButtonAction_Click);
        }

        public void SetTask(Task task)
        {
            this.task = task;
            taskType = task.taskType;
            socialsURL = task.SocialsURL;

            descriptionText.text = task.description;
            rewardText.text = $"{task.Rewards / 1000}K $Space";
        }

        private void TaskButtonAction_Click()
        {
            if(taskType == TaskType.Socials)
            {
                task.IncreaseTargetValue();
                Application.OpenURL(socialsURL);
            }

            else if(taskType == TaskType.Referral)
            {
                //Open Friends Page
                gameObject.SetActive(false);
                TaskPanel.Instance.friendPanel.gameObject.SetActive(true);
            }
        }
    }
}
