using UnityEngine;

namespace Onion_AI
{
    [System.Serializable]
    public class Task
    {
        [Header("Status")]
        public bool isCompleted;
        public TaskType taskType;
        public TaskRewards taskRewards;

        [field: Header("Stats")]
        private int currentValue;
        [SerializeField] private int targetValue;
        [SerializeField] private int targetWaveValue; //KillEnemies Only
        [field: SerializeField] public int Rewards {get; private set;}

        [field: Header("Task Info")]
        [SerializeField] private string title;
        [TextArea(3, 5)] public string description;
        [field: SerializeField] public string SocialsURL {get; private set;} //Socials

        public Task(int reward, string taskTitle, string taskDescription, TaskRewards taskRewardType)
        {
            Rewards = reward;
            title = taskTitle;
            isCompleted = false;
            description = taskDescription;
            taskRewards = taskRewardType;
        }

        public void CreateNewTask()
        {
            if(taskType != TaskType.KillEnemies)
            {
                return;
            }

            if(isCompleted != true)
            {
                return;
            }

            Task newTask = new Task
            (
                Rewards + 1000,
                "KillEnemyTask", 
                $"Kill {targetValue + 100} Enemies and Reach Wave ",
                TaskRewards.Coin
            );
            PlayerTaskManager.Instance.AdNewTask(newTask);
        }

        public void IncreaseTargetValue()
        {
            if(isCompleted)
            {
                return;
            }

            currentValue++;
            isCompleted = (currentValue >= targetValue);
        }
    }
}
