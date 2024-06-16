using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _steeringAngle = 4.0f;
    [SerializeField] private CarMovement _carMovement;
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private float _wheelRotationSpeed = 100.0f;

    private float _currentSpeed;
    private bool _isMoving = true;
    private ParticleSystem _explotion;
    private Rigidbody _rigidbody;

    public event Action OnCrash;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _explotion = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void GetCarMovement(CarMovement carMovement)
    {
        _carMovement = carMovement;
    }

    public void Die()
    {
        _isMoving = false;
        _explotion.Play();
        Invoke("TurnOffCar", 0.7f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out EnemyMovement enemy))
        {
            Die();
            OnCrash?.Invoke();
        }
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
