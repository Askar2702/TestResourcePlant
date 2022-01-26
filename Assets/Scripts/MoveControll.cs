using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControll : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private JoystickUi _joystick;
    private Quaternion _rotate;
    private Vector3 _stopPos;

    Vector3 _move = Vector3.zero;

    private void Start()
    {
        _stopPos = transform.position;
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (_joystick.isMove)
        {
            _move = new Vector3(_joystick.JoystickPos.x, 0f, _joystick.JoystickPos.y);
            transform.LookAt(transform.position + _move);
            _rotate = transform.rotation;
            _rb.velocity = _move * Time.deltaTime * _speed;
            _stopPos = transform.position;

        }
        else
        {
            transform.position = new Vector3(_stopPos.x, transform.position.y, _stopPos.z);
            transform.rotation = _rotate;
        }
        transform.position = new Vector3(transform.position.x, 0.8f, transform.position.z);
    }
   
}
