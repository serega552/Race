using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private readonly int MenuState = Animator.StringToHash("MenuCamera");

    [SerializeField] private Vector3 _offSet;
    [SerializeField] private Quaternion _rotation = Quaternion.Euler(44f, -151f, 0f);
    [SerializeField] private float _speed = 5f;

    private Animator _animator;
    private Transform _car;
    private bool _isMove = false;
    private WaitForSeconds _waitEndGame = new WaitForSeconds(1f);
    private Coroutine _startEndGameCoroutine;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        SetStartPosition();
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

    public void ResetCameraPosition()
    {
        SetStartPosition();
    }

    public void EndMove()
    {
        _isMove = false;
        _startEndGameCoroutine = StartCoroutine(EndTime());
    }

    public void GetPlayerTransform(Transform transform)
    {
        _car = transform;
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
            Vector3 newCamPosition = new Vector3(_car.position.x + _offSet.x, _car.position.y + _offSet.y, _car.position.z + _offSet.z);
            Quaternion newCamRotation = _rotation;
            transform.position = Vector3.Lerp(transform.position, newCamPosition, _speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newCamRotation, _speed * Time.deltaTime);
        }
    }

    private IEnumerator EndTime()
    {
        yield return _waitEndGame;
        SetStartPosition();
        StopCoroutine(_startEndGameCoroutine);
    }
}
