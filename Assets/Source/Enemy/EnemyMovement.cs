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

        public event Action<Transform> Crashing;

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
                Crashing?.Invoke(transform);
                UnMove();
            }

            if (collision.collider.TryGetComponent(out EnemyMovement enemy))
            {
                Crashing?.Invoke(transform);
                UnMove();
            }
        }

        public void ChangeCarMovement(CarMovement carMovement)
        {
            _carMovement = carMovement;
        }

        public void UnMove()
        {
            _isMoving = false;
            gameObject.SetActive(false);
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
    }
}
