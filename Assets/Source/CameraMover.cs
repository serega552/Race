using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private Quaternion _rotation = Quaternion.Euler(44f, -151f, 0f);
    [SerializeField] private float _speed = 5f;

    private Transform _car;
    private bool _isMove = true;
    private Vector3 _startPosition = new Vector3(-1.213f, -34.5f, 17.892f);

    private void Awake()
    {
        _startPosition = transform.position;

        SetStartPosition();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void StartMove()
    {
        _isMove = true;
    }

    public void ResetCameraPosition()
    {
        SetStartPosition();
    }

    public void EndMove()
    {
        _isMove = false;
    }

    public void GetPlayerTransform(Transform transform)
    {
        _car = transform;
    }

    private void SetStartPosition()
    {
        transform.position = _startPosition;
    }

    private void Move()
    {
        if (_isMove)
        {
            Vector3 newCamPosition = new Vector3(_car.position.x + _offSet.x, _car.position.y + _offSet.y, _car.position.z + _offSet.z);
            Quaternion newCamRotation = _rotation;
            transform.position = Vector3.Lerp(transform.position, newCamPosition, _speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newCamRotation, _speed * Time.deltaTime);
        }
    }
}
