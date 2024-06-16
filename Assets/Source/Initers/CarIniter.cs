using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarIniter : MonoBehaviour
{
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private SkinSelecter _skinSelecter;

    private CarMovement _carMovement;

    private void OnEnable()
    {
        _skinSelecter.OnChangingSkin += Init;
    }

    private void OnDisable()
    {
        _skinSelecter.OnChangingSkin -= Init;
    }

    public void Init(CarMovement carMovement)
    {
        Debug.Log(carMovement);

        _carMovement = carMovement;

        _cameraMover.GetPlayerTransform(_carMovement.transform);
        _spawner.Init(_carMovement);

        _carMovement.gameObject.SetActive(true);
    }
}
