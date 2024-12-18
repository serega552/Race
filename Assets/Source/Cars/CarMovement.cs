using Audio;
using Blocks;
using Enemy;
using PlayerInputSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarMovement : MonoBehaviour
    {
        private readonly List<ParticleSystem> _waterParticles = new List<ParticleSystem>();
        private readonly float _startPositionSumY = 0.5f;
        private readonly float _maxDistanceRay = 1f;
        
        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private float _steeringAngle = 4.0f;
        [SerializeField] private Transform[] _wheels;
        [SerializeField] private float _wheelRotationSpeed = 100.0f;
        [SerializeField] private ParticleSystem[] _wheelEffects;
        [SerializeField] private ParticleSystem _waterParticle;
        [SerializeField] private SoundSwitcher _audioManager;
        [SerializeField] private PlayerInput _playerInput;

        private Vector3 _startPosition;
        private Vector3 _startSpawnPosition = Vector3.zero;
        private Quaternion _startRotation = new Quaternion(0f, 0f, 0f, 0f);
        private bool _isMove = false;
        private bool _canPlay = false;
        private float _currentSpeed;
        private ParticleSystem _explotion;
        private float _verticalInput;
        private float _horizontalInput;
        private Rigidbody _rigidbody;

        public event Action EndMoving;

        private void Awake()
        {
            _startSpawnPosition = transform.position;

            _rigidbody = GetComponent<Rigidbody>();
            _explotion = GetComponentInChildren<ParticleSystem>();
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
                enemy.UnMove();
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

            EndMoving?.Invoke();
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
            _playerInput.UseCurrentInput();
            _horizontalInput = _playerInput.HorizonInput;
            _verticalInput = _playerInput.VerticalInput;

            if (_verticalInput != 0)
                _audioManager.ChangePitch("StartCar", 0.2f);
            else
                _audioManager.ChangePitch("StartCar", -1.5f);

            _currentSpeed = _verticalInput * _speed;

            _rigidbody.velocity += _rigidbody.transform.forward * _currentSpeed * Time.deltaTime;

            _rigidbody.transform.Rotate(Vector3.up, _horizontalInput * _steeringAngle);

            Vector3 rotateWheel = Vector3.right * _currentSpeed * Time.deltaTime * _wheelRotationSpeed;

            if (Input.GetKey(KeyCode.Space))
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * 2);
            }

            for (int i = 0; i < _wheels.Length; i++)
            {
                _wheels[i].Rotate(rotateWheel);
            }

            if (_currentSpeed != 0)
            {
                foreach (var effect in _wheelEffects)
                {
                    effect.Play();
                }
            }
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





