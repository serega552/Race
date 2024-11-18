using UnityEngine;

namespace Tasks.TaskFactory
{
    public abstract class TasksFactory : MonoBehaviour
    {
        public abstract TaskView CreateTask();
    }
}