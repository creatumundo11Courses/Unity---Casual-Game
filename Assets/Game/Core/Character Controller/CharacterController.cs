using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private InputListenerSO _input;
    [SerializeField]
    private float _forwardSpeed;
    [SerializeField]
    private float _horizontalSpeed;
    private Vector2 _desiredMovementV;
    void Start()
    {
        _input.OnMoveEvent += Move;
    }
    private void Update()
    {
        Vector3 movementV = new Vector3(_desiredMovementV.x,0, _desiredMovementV.y);
        movementV.Normalize();
        movementV.z *= _forwardSpeed;
        movementV.x *= _horizontalSpeed;
        movementV *= Time.deltaTime;
        transform.Translate(movementV);
    }
    private void Move(Vector2 vector)
    {
        _desiredMovementV = vector;
    }
}
