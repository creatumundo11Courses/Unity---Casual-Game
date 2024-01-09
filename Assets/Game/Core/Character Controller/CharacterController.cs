using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterMove _characterMove;
    [SerializeField]
    private InputListenerSO _input;
    private Vector3 _desiredMovementV;
    void Start()
    {
        _input.OnMoveEvent += Move;
    }
    private void Update()
    {
        _characterMove.Move(_desiredMovementV);
    }
    private void Move(Vector2 moveV)
    {
        _desiredMovementV = new Vector3(moveV.x, 0 , moveV.y);
    }
}
