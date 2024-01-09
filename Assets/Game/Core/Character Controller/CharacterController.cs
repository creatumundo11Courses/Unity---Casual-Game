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
    private CharacterInput _characterInput;
    [SerializeField]
    private InputListenerSO _inputListener;
    private Vector3 _desiredMovementV;
    void Start()
    {
        IInput input = new PlayerInput(_inputListener, this);
        _characterInput.Initialize(input);
    }
    private void Update()
    {
        _characterMove.Move(_desiredMovementV);
    }
    public void Move(Vector2 moveV)
    {
        _desiredMovementV = new Vector3(moveV.x, 0 , moveV.y);
    }
}
