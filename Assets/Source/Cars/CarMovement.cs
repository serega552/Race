using Audio;
using Blocks;
using Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Cars
{
    [RequireComponent(typeof(Rigidbody))]

    public class CarMovement : MonoBehaviour
    {
        private readonly List<ParticleSystem> _waterParticles = new List<ParticleSystem>();
        
        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private float _steeringAngle = 4.0f;
        [SerializeField] private Transform[] _wheels;
        [SerializeField] private float _wheelRotationSpeed = 100.0f;
        [SerializeField] private ControlButton _leftButton;
        [SerializeField] private ControlButton _rightButton;
        [SerializeField] private ControlButton _upButton;
        [SerializeField] private ControlButton _downButton;
        [SerializeField] private ParticleSystem[] _wheelEffects;
        [SerializeField] private ParticleSystem _waterParticle;
        [SerializeField] private AudioManager _audioManager;

        private Vector3 _startPosition;
        private float _startPositionSumY = 0.5f;
        private float _maxDistanceRay = 1f;
        private Vector3 _startSpawnPosition = Vector3.zero;
        private Quaternion _startRotation = new Quaternion(0f, 0f, 0f, 0f);
        private bool _isMove = false;
        private bool _canPlay = false;
        private float _currentSpeed;
        private ParticleSystem _explotion;
        private float _verticalInput;
        private float _horizontalInput;
        private bool _isMobile = false;
        private Rigidbody _rigidbody;

        public event Action OnEndMove;

        private void Awake()
        {
            _startSpawnPosition = transform.position;

            _rigidbody = GetComponent<Rigidbody>();
            _explotion = GetComponentInChildren<ParticleSystem>();

            _isMobile = YandexGame.EnvironmentData.isMobile;
        }

        private void FixedUpdate()
        {
            if (_isMove && _canPlay)
                Move();

            if (_canPlay)
                CheckGround();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.TryGetComponent(out EnemyMovement enemy) && _canPlay)
            {
                enemy.Die();
                Crash();
            }
        }

        public void ResetCar()
        {
            _currentSpeed = 0;
            _verticalInput = 0;
            _horizontalInput = 0;
            _rigidbody.velocity = new Vector3();

            transform.position = _startSpawnPosition;
            transform.rotation = _startRotation;

            foreach (var particle in _waterParticles)
            {
                Destroy(particle.gameObject);
            }

            _waterParticles.Clear();
        }

        public void EndMove()
        {
            _canPlay = false;
            _audioManager.Stop("StartCar");
            _isMove = false;

            OnEndMove?.Invoke();
        }

        public void StartMove()
        {
            _wheelEffects[0].Play();
            _wheelEffects[1].Play();
            _canPlay = true;
            _isMove = true;
            _audioManager.Play("StartCar");
        }

        public void Resurrect()
        {
            _canPlay = true;
            _isMove = true;
            ResetCar();
            _audioManager.Play("StartCar");
        }

        private void Move()
        {
            if (_isMobile)
                MobileMove();
            else
                DesktopMove();

            if (_verticalInput != 0)
                _audioManager.ChangePitch("StartCar", 0.2f);
            else
                _audioManager.ChangePitch("StartCar", -1.5f);

            _currentSpeed = _verticalInput * _speed;

            _rigidbody.velocity += _rigidbody.transform.forward * _currentSpeed * Time.deltaTime;

            _rigidbody.transform.Rotate(Vector3.up, _horizontalInput * _steeringAngle);

            if (Input.GetKey(KeyCode.Space))
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * 2);
            }

            for (int i = 0; i < _wheels.Length; i++)
            {
                _wheels[i].Rotate(Vector3.right * _currentSpeed * Time.deltaTime * _wheelRotationSpeed);
            }

            if (_currentSpeed != 0)
            {
                _wheelEffects[0].Play();
                _wheelEffects[1].Play();
            }
        }

        private void DesktopMove()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }

        private void MobileMove()
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

        private void Crash()
        {
            if (_canPlay)
            {
                _canPlay = false;
                EndMove();
                _explotion.Play();
            }
        }

        private void CheckGround()
        {
            _startPosition = new Vector3(transform.position.x, transform.position.y + _startPositionSumY, transform.position.z);
            Ray ray = new Ray(_startPosition, transform.up * -1);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistanceRay))
            {
                if (hit.collider.TryGetComponent(out Block block))
                    _isMove = true;

                if (hit.collider.TryGetComponent(out WaterBlock waterBlock) && _canPlay)
                {
                    var particle = Instantiate(_waterParticle, transform.position, waterBlock.transform.localRotation);
                    _waterParticles.Add(particle);
                    EndMove();
                }
            }
            else
            {
                _isMove = false;
            }
        }
    }
}





