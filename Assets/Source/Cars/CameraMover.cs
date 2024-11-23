using PlayerInputSystem;
using UnityEngine;

namespace Cars
{
    public class CameraMover : MonoBehaviour
    {
        private readonly int _menuState = Animator.StringToHash("MenuCamera");
        private readonly float _camRotationX = 42f;
        private readonly float _camRotationZ = 358f;
        private readonly float _camPositionVerticalMultiply = 2;

        [SerializeField] private Vector3 _offSet;
        [SerializeField] private Quaternion _rotation;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private PlayerInput _playerInput;

        private Animator _animator;
        private Transform _car;
        private bool _isMove = false;
        private float _verticalInput;
        private float _horizontalInput;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            SetStartPosition();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void StartMove()
        {
            _animator.cullingMode = AnimatorCullingMode.CullCompletely;
            transform.rotation = _rotation;
            _isMove = true;
        }

        public void EndMove()
        {
            _isMove = false;
            SetStartPosition();
        }

        public void ChangePlayerTransform(Transform transform)
        {
            _car = transform;
        }

        public void ControlCamera()
        {
            _playerInput.UseCurrentInput();
            _horizontalInput = _playerInput.HorizonInput;
            _verticalInput = _playerInput.VerticalInput;
        }

        private void SetStartPosition()
        {
            _animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            _animator.Play(_menuState);
        }

        private void Move()
        {
            if (_isMove)
            {
                ControlCamera();
                Vector3 newCamPosition = new Vector3(
                    _car.position.x + _offSet.x,
                    _car.position.y + _offSet.y,
                    _car.position.z + _offSet.z);
                Quaternion newCamRotation = _rotation;

                if (_horizontalInput != 0)
                {
                    newCamRotation = Quaternion.Euler(
                        _camRotationX,
                        _rotation.y + _horizontalInput,
                        _camRotationZ);
                }

                if (_verticalInput != 0)
                {
                    newCamPosition = new Vector3(
                        _car.position.x + _offSet.x,
                        _car.position.y + _offSet.y + _verticalInput * _camPositionVerticalMultiply,
                        _car.position.z + _offSet.z + -_verticalInput * _camPositionVerticalMultiply);
                }

                transform.position = Vector3.Lerp(transform.position, newCamPosition, _speed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, newCamRotation, _speed * Time.deltaTime);
            }
        }
    }
}