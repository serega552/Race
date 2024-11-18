using System.Collections.Generic;
using Tasks.SO;
using Tasks.TaskFactory;
using UnityEngine;

namespace Tasks.Spawner
{
    public abstract class TaskSpawner : MonoBehaviour
    {
        private readonly List<TaskView> _tasksActive = new List<TaskView>();
        private readonly Dictionary<int, float> _activeDailyId = new Dictionary<int, float>();
        
        [SerializeField] private GameObject _prefabTask;
        [SerializeField] private Transform _contentTasks;
        [SerializeField] private List<Task> _tasks = new List<Task>();
        [SerializeField] private TaskTimeInspector _timeInspector;
        [SerializeField] private  DefaultTasksFactory _defaultTasksFactory;

        protected List<float> AmountProgreses = new List<float>();

        public List<TaskView> ActiveTasks => _tasksActive;
        public TaskTimeInspector TaskInspector => _timeInspector;

        private void Awake()
        {
            TaskCounter.Init();
            SpawnTasks();
        }

        private void OnDisable()
        {
            foreach (var task in _tasksActive)
            {
                task.Completed -= DestroyTask;
            }
        }

        public void SpawnTasks()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                TaskView taskView = _defaultTasksFactory.CreateTask();

                taskView.transform.SetParent(_contentTasks);
                taskView.GetTask(_tasks[i]);
                taskView.Init();
                taskView.InitId(i);
                taskView.gameObject.SetActive(true);
                _tasksActive.Add(taskView);
                Save();
            }

            foreach (var task in _tasksActive)
            {
                task.Completed += DestroyTask;
            }

            Load();
        }

        public virtual void RefreshTasks()
        {
            _activeDailyId.Clear();

            foreach (var task in _tasksActive)
            {
                Destroy(task.gameObject);
            }

            _tasksActive.Clear();

            SpawnTasks();
        }

        public abstract void Save();

        public virtual void Load()
        {
            for (int i = 0; i < _tasksActive.Count; i++)
            {
                _tasksActive[i].InitProgress(AmountProgreses[i]);
            }

            for (int i = 0; i < _tasksActive.Count; i++)
            {
                if (AmountProgreses[i] == -1)
                {
                    _tasksActive[i].gameObject.SetActive(false);
                }
            }
        }

        private void DestroyTask(TaskView taskView)
        {
            taskView.gameObject.SetActive(false);
        }
    }
}
