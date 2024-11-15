using Cars;
using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _steeringAngle = 4.0f;
        [SerializeField] private CarMovement _carMovement;
        [SerializeField] private Transform[] _wheels;
        [SerializeField] private float _wheelRotationSpeed = 100.0f;

        private float _currentSpeed;
        private bool _isMoving = true;
        private Rigidbody _rigidbody;

        public event Action<Transform> OnCrash;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out WaterBlock block))
            {
                OnCrash?.Invoke(transform);
                Die();
            }

            if (collision.collider.TryGetComponent(out EnemyMovement enemy))
            {
                OnCrash?.Invoke(transform);
                Die();
            }
        }

        public void GetCarMovement(CarMovement carMovement)
        {
            _carMovement = carMovement;
        }

        public void Die()
        {
            _isMoving = false;
            TurnOffCar();
        }

        private void Move()
        {
            if (_isMoving)
            {
                _currentSpeed = _speed * Time.deltaTime;
                _rigidbody.velocity += _rigidbody.transform.forward * _currentSpeed * Time.deltaTime;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, (_carMovement.transform.position - transform.position), _steeringAngle, 0.0F);
                Quaternion rotation = Quaternion.LookRotation(newDir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _steeringAngle * Time.deltaTime);

                for (int i = 0; i < _wheels.Length; i++)
                {
                    _wheels[i].Rotate(Vector3.right * _currentSpeed * Time.deltaTime * _wheelRotationSpeed);
                }
            }
        }

        private void TurnOffCar()
        {
            gameObject.SetActive(false);
        }
    }
}
