using UnityEngine;
using YG;

namespace Cars
{
    public class CameraMover : MonoBehaviour
    {
        private readonly int MenuState = Animator.StringToHash("MenuCamera");

        [SerializeField] private ControlButton _leftButton;
        [SerializeField] private ControlButton _rightButton;
        [SerializeField] private ControlButton _upButton;
        [SerializeField] private ControlButton _downButton;
        [SerializeField] private Vector3 _offSet;
        [SerializeField] private Quaternion _rotation;
        [SerializeField] private float _speed = 5f;

        private Animator _animator;
        private Transform _car;
        private bool _isMove = false;
        private float _verticalInput;
        private float _horizontalInput;
        private bool _isMobile = false;
        private float _camRotationX = 42f;
        private float _camRotationZ = 358f;
        private float _camPositionVerticalMultiply = 2;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            SetStartPosition();
        }

        private void Awake()
        {
            _isMobile = YandexGame.EnvironmentData.isMobile;
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
            if (_isMobile)
                InputMobile();
            else
                InputDesktop();
        }

        private void InputDesktop()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }

        private void InputMobile()
        {
            if (_leftButton.IsHold)
                _horizontalInput = -1f;
            else if (_rightButton.IsHold)
                _horizontalInput = 1f;
            else
                _horizontalInput = 0f;

            if (_upButton.IsHold)
                _verticalInput = 1f;
            else if (_downButton.IsHold)
                _verticalInput = -1f;
            else
                _verticalInput = 0f;
        }

        private void SetStartPosition()
        {
            _animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            _animator.Play(MenuState);
        }

        private void Move()
        {
            if (_isMove)
            {
                ControlCamera();
                Vector3 newCamPosition = new Vector3(_car.position.x + _offSet.x, _car.position.y + _offSet.y, _car.position.z + _offSet.z);
                Quaternion newCamRotation = _rotation;

                if (_horizontalInput != 0)
                    newCamRotation = Quaternion.Euler(_camRotationX, _rotation.y + _horizontalInput, _camRotationZ);

                if (_verticalInput != 0)
                    newCamPosition = new Vector3(_car.position.x + _offSet.x, _car.position.y + _offSet.y + _verticalInput * _camPositionVerticalMultiply, _car.position.z + _offSet.z + -_verticalInput * _camPositionVerticalMultiply);

                transform.position = Vector3.Lerp(transform.position, newCamPosition, _speed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, newCamRotation, _speed * Time.deltaTime);
            }
        }
    }
}