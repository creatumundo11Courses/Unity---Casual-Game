using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField]
    private CharacterControllerBase _controller;
    [SerializeField]
    private Animator _animatorTarget;
    [SerializeField]
    private string _idleAnimationName;
    [SerializeField]
    private string _runAnimationName;

    private void Start()
    {
        if(_controller != null) SetController(_controller);
    }

    public void SetController(CharacterControllerBase controller)
    {
        if (controller == null) return;
        if (_controller != null)
        {
            _controller.OnMove -= OnMoveAnimation;
        }

        _controller = controller;
        _controller.OnMove += OnMoveAnimation;
    }

    private void OnMoveAnimation(float vMagnitude)
    {
        if (_controller == null) return;

        if (vMagnitude > 0)
        {
            PlayAnimation(_runAnimationName);
        }
        else
        {
            PlayAnimation(_idleAnimationName);
        }
    }

    private void PlayAnimation(string animationName)
    {
        _animatorTarget.Play(animationName);
    }

    public void RemoveController()
    {
        if (_controller != null)
        {
            _controller.OnMove -= OnMoveAnimation;
        }

        _controller = null;
    }
}
