using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    private IInput _input;

    public void Initialize(IInput input)
    {
        _input = input;
        _input.Initialize();
    }

    private void Update()
    {
        if (_input == null) return;
        _input.Update();
    }

    private void OnDestroy()
    {
        _input.Terminate();
    }
}

public interface IInput
{
    void Initialize();
    void Terminate();
    void Update();
}

public class PlayerInput : IInput
{
    private InputListenerSO _inputListener;
    private CharacterController _player;
    public PlayerInput(InputListenerSO inputListenet, CharacterController player)
    {
        _inputListener = inputListenet;
        _player = player;
    }
    public void Initialize()
    {
        _inputListener.OnMoveEvent += _player.Move;
    }

    public void Terminate()
    {
        _inputListener.OnMoveEvent -= _player.Move;
    }

    public void Update()
    {
        
    }
}