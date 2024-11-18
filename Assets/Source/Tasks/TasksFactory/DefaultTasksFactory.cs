using Tasks.SO;
using UnityEngine;

namespace Tasks.TaskFactory
{
    public class DefaultTasksFactory : TasksFactory
    {
        [SerializeField] private TaskView _prefab;
        [SerializeField] private Transform _content;

        public override TaskView CreateTask()
        {
            return Instantiate(_prefab, _content, false);
        }
    }
}