using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public static PlayerInputHandler Instance;

        [SerializeField] private InputActionAsset _playerControls;
        [SerializeField] private string _actionMapName = "Car";
        [SerializeField] private string _move = "Move";
        [SerializeField] private string _pause = "Pause";

        private InputAction _moveAction;
        private InputAction _pauseAction;

        public event Action OnPauseButtonClick;

        public Vector3 MoveInput { get; private set; }

        private void OnEnable()
        {
            _moveAction.Enable();
            _pauseAction.Enable();
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_move);
            _pauseAction = _playerControls.FindActionMap(_pause).FindAction(_pause);

            RegisterInputActions();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
        }

        private void RegisterInputActions()
        {
            _moveAction.performed += context => MoveInput = context.ReadValue<Vector3>();
            _moveAction.canceled += context => MoveInput = Vector2.zero;

            _pauseAction.performed += context => OnPauseButtonClick?.Invoke();
        }
    }
}