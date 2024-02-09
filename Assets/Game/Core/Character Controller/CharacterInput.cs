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
        if (_input != null)
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
    private CharacterControllerPlayer _player;
    public PlayerInput(InputListenerSO inputListenet, CharacterControllerPlayer player)
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

public class IAInput : IInput
{
    private CharacterControllerIA _characterController;
    public IAInput(CharacterControllerIA characterControllerIA)
    {
        _characterController = characterControllerIA;
    }
    public void Initialize()
    {
        _characterController.Target = null;
    }

    public void Terminate()
    {
        
    }

    public void Update()
    {
        if (_characterController.Target == null)
        {
            _characterController.Stop();
            return;
        }


        Vector3 direction = (_characterController.Target.position - _characterController.transform.position).normalized;
        direction.y = 0;
        Vector2 inputV = new Vector2(direction.x, direction.z);
        _characterController.Move(inputV);
    }
}