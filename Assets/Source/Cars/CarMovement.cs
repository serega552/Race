using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] private List<ParticleSystem> _wheelEffects;

    private Vector3 _startPosition;
    private Vector3 _startSpawnPosition = new Vector3(0f, 0f, 0f);
    private Quaternion _startRotation = new Quaternion(0f, 0f, 0f, 0f);
    private bool _isMove = false;
    private bool _canPlay = false;
    private float _currentSpeed;
    private ParticleSystem _explotion;
    private float _verticalInput;
    private float _horizontalInput;
    private bool _isMobile = false;
    private Rigidbody _rigidbody;

    public event Action OnEndGame;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _explotion = GetComponentInChildren<ParticleSystem>();

        _isMobile = YandexGame.EnvironmentData.isMobile;
    }

    private void FixedUpdate()
    {
        if (_isMove && _canPlay)
            Move();

        if(_canPlay)
            CheckGround();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.TryGetComponent(out EnemyMovement enemy))
        {
            Crash();
        }
    }

    private void CheckGround()
    {
        _startPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Ray ray = new Ray(_startPosition, transform.up * -1);

        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            if (hit.collider.TryGetComponent(out Block block))
                _isMove = true;
        }
        else
        {
            _isMove = false;
        }
        Debug.DrawRay(_startPosition, transform.up * -1, Color.red);     
    }

    public void ResetCar()
    {
        transform.position = _startSpawnPosition;
        transform.rotation = _startRotation;
    }

    public void EndMove()
    {
        _canPlay = false;
        _isMove = false;
        AudioManager.Instance.ResetPitch("StartCar");
        OnEndGame?.Invoke();
    }

    private void Move()
    {
        if (_isMobile)
            MobileMove();
        else
            DesktopMove();

        if(_verticalInput != 0)
            AudioManager.Instance.ChangePitch("StartCar", 0.2f);
        else
            AudioManager.Instance.ChangePitch("StartCar", -1.5f);

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

        if(_currentSpeed != 0)
        {
            _wheelEffects[0].Play();
            _wheelEffects[1].Play();
        }
        else
        {
            _wheelEffects[0].Stop();
            _wheelEffects[1].Stop();
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
        _canPlay = true;
        _isMove = true;
        AudioManager.Instance.Play("StartCar");
    }
}





