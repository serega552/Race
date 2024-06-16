using System;
using UnityEngine;
using YG;

[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _steeringAngle = 4.0f;
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private float _wheelRotationSpeed = 100.0f;
    [SerializeField] private ControlButton _leftButton;
    [SerializeField] private ControlButton _rightButton;
    [SerializeField] private ControlButton _upButton;
    [SerializeField] private ControlButton _downButton;

    private Vector3 _startPosition = new Vector3(0f, 0f, 0f);
    private Quaternion _startRotation = new Quaternion(0f, 0f, 0f, 0f);
    private bool _isMove = false;
    private float _currentSpeed;
    private ParticleSystem _explotion;
    private float _verticalInput;
    private float _horizontalInput;
    private bool _isMobile = false;

    public event Action OnEndGame;

    private void Awake()
    {
        _explotion = GetComponentInChildren<ParticleSystem>();

        _isMobile = YandexGame.EnvironmentData.isMobile;
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

    private void Move()
    {
        if (_isMobile)
            MobileMove();
        else
            DesktopMove();

        _currentSpeed = _verticalInput * _speed;
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;

        if (_currentSpeed != 0)
            transform.Rotate(Vector3.up, _horizontalInput * _steeringAngle);

        if (Input.GetKey(KeyCode.Space))
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * 2);
        }

        for (int i = 0; i < _wheels.Length; i++)
        {
            _wheels[i].Rotate(Vector3.right * _currentSpeed * Time.deltaTime * _wheelRotationSpeed);
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
        EndMove();
        _explotion.Play();
    }

    public void StartMove()
    {
        _isMove = true;
    }
}





