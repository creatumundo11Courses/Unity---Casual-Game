using UnityEngine.InputSystem;
using UnityEngine;
using static PlayerInput;
using System;

[CreateAssetMenu(menuName = "Course/InputListener",fileName = "new Input Listener")]
public class InputListenerSO : ScriptableObject, IInGameActions
{
    private PlayerInput _playerInput;

    public event Action<Vector2> OnMoveEvent;

    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.InGame.SetCallbacks(this);
            SetInGameInput();
        }
    }

    private void SetInGameInput()
    {
        _playerInput.InGame.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Debug.Log(value);
        OnMoveEvent?.Invoke(value);
    }
}
