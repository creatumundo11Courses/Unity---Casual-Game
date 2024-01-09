using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerPlayer : CharacterControllerBase
{
    [SerializeField]
    private InputListenerSO _inputListener;

    public override void OnInitialize()
    {
        IInput input = new PlayerInput(_inputListener, this);
        _characterInput.Initialize(input);
    }
    
}
