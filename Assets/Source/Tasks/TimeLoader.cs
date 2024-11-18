using UnityEngine;

namespace Tasks
{
    public class TimeLoader : MonoBehaviour
    {
        [SerializeField] private TaskTimeInspector _taskInspector;

        private void Start()
        {
            _taskInspector.Load();
            _taskInspector.RefreshTime();
        }
    }
}
