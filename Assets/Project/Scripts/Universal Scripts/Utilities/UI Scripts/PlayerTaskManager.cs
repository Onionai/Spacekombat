using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class PlayerTaskManager : MonoBehaviour
    {
        [System.Serializable]
        private class PlayerTaskManagerWrapper
        {
            public List<Task> tasks;
        }

        public static PlayerTaskManager Instance;
        public List<Task> taskList = new List<Task>();
        private const string TaskDataKey = "NewTaskData";


        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }

            LoadTasks();
            SaveTasks();
        }

        public void AdNewTask(Task task)
        {
            taskList.Add(task);
            SaveTasks();
        }

        private void Update()
        {
            if(TaskPanel.Instance != null)
            {
                TaskPanel.Instance.RefreshTaskPanel(taskList);
            }
        }

        public void PopulateTaskList(UserProfile userProfile)
        {
            taskList.Clear();
            taskList.AddRange(userProfile.taskList);
        }

        public void UpdateAllTasks(TaskRewards taskRewards)
        {
            foreach(Task task in taskList)
            {
                if(task.taskRewards == taskRewards)
                {
                    task.IncreaseTargetValue();
                }
            }
        }

        public void RemoveAllCompletedTasks()
        {
            for(int i = 0; i < taskList.Count; i++)
            {
                if(taskList[i].isCompleted == true)
                {
                    taskList.Remove(taskList[i]);
                }
            }
        }

        private void SaveTasks()
        {
            string json = JsonUtility.ToJson(new PlayerTaskManagerWrapper {tasks = taskList});
            PlayerPrefs.SetString(TaskDataKey, json);
            PlayerPrefs.Save();
        }

        private void LoadTasks()
        {
            if(PlayerPrefs.HasKey(TaskDataKey))
            {
                string json = PlayerPrefs.GetString(TaskDataKey);
                var wrapper = JsonUtility.FromJson<PlayerTaskManagerWrapper>(json);
                taskList = wrapper.tasks ?? new List<Task>();
            }
        }
    }
}
