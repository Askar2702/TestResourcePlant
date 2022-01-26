using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickUi : MonoBehaviour
{
    [SerializeField] private int _movementRange = 100;
    [SerializeField] private GameObject _circle;
    public bool isMove { get; private set; }
    public Vector2 JoystickPos { get; private set; }
    private Vector3 _startPos;

    // Update is called once per frame
    private void Start()
    {
        isMove = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //  gameObject.SetActive(true);
            transform.position = Input.mousePosition;
            _circle.transform.position = Input.mousePosition;
            _startPos = transform.position;
            isMove = true;

        }
        else if (Input.GetMouseButton(0))
        {
            MoveDot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // gameObject.SetActive(false);
            transform.position = _startPos;
            isMove = false;
        }
    }
    private void MoveDot()
    {
        transform.position = Input.mousePosition;
        int deltaX = (int)(transform.position.x - _startPos.x);
        int deltaY = (int)(transform.position.y - _startPos.y);
        Vector3 newPos = Vector3.zero;
        newPos.x = deltaX;
        newPos.y = deltaY;
        transform.position = Vector3.ClampMagnitude(new Vector3(newPos.x, newPos.y, newPos.z), _movementRange) + _startPos;
        JoystickPos = (transform.position - _circle.transform.position).normalized;
    }
}
