using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _steeringAngle = 4.0f;
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private float _wheelRotationSpeed = 100.0f;

    private Vector3 _startPosition = new Vector3(0f, 0f, 0f);
    private Quaternion _startRotation = new Quaternion(0f, 0f, 0f, 0f);
    private bool _isMove = false;
    private float _currentSpeed;
    private ParticleSystem _explotion;

    public event Action OnEndGame;

    private void Awake()
    {
        _explotion = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (_isMove)
            Move();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.TryGetComponent(out EnemyMovement enemy))
        {
            Crash();
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _currentSpeed = verticalInput * _speed;
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;

        if (_currentSpeed != 0)
        {
            transform.Rotate(Vector3.up, horizontalInput * _steeringAngle);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * 2);
        }

        for (int i = 0; i < _wheels.Length; i++)
        {
            _wheels[i].Rotate(Vector3.right * _currentSpeed * Time.deltaTime * _wheelRotationSpeed);
        }
    }

    private void Crash()
    {
        EndMove();
        _explotion.Play();
    }

    public void StartMove()
    {
        _isMove = true;
    }

    public void ResetCar()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }

    public void EndMove()
    {
        _isMove = false;
        OnEndGame?.Invoke();
    }
}





