using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _cameraOffset;
    private Camera _camera;
    [Range(0.01f, 1.0f)]
    [SerializeField] private float _smoothFactor = 0.5f;
    private bool isLook = false;
    private void Start()
    {
        _camera = Camera.main;
        _cameraOffset = _camera.transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = _target.position + _cameraOffset;
        _camera.transform.position = Vector3.Slerp(_camera.transform.position, newPos, _smoothFactor);
        if (isLook)
            _camera.transform.LookAt(_target);
    }
}
