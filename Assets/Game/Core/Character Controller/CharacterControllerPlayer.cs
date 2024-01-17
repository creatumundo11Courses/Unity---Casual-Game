using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerPlayer : CharacterControllerBase
{
    private const float REPEAT_RATE_TO_ORGANIZE = 0.4f;
    private bool _isOrganized;
    private float _currentTimeRepeatRateToOrganize = 0;
    [SerializeField]
    private InputListenerSO _inputListener;
    [SerializeField]
    private Multiplicable _multiplicable;
    [SerializeField]
    private CircleLayout _circleLayout;
    [SerializeField]
    private bool autoForwardMovement;

    private void Awake()
    {
        _multiplicable.OnCreate += OnCreateImpostors;
        _multiplicable.OnCreateDone += OnCreateImpostorsDone;
        _multiplicable.OnDestroy += OnDestroyImpostors;
    }


    public override void OnUpdate()
    {
        ClampMoveV(ref _desiredMovementV);
        if (autoForwardMovement) _desiredMovementV.z = 1;

        base.OnUpdate();

        if (!_isOrganized)
        {
            _currentTimeRepeatRateToOrganize += Time.deltaTime;
            if (_currentTimeRepeatRateToOrganize >= REPEAT_RATE_TO_ORGANIZE)
            {
                _circleLayout.Organize();
                _isOrganized = true;
                _currentTimeRepeatRateToOrganize = 0;
            }
        }
    }

    private void ClampMoveV(ref Vector3 desiredMovementV)
    {
        bool isLeftLimit = IsInLimitPointByRay(Vector3.left);
        bool isRightLimit = IsInLimitPointByRay(Vector3.right);
        if(isLeftLimit && desiredMovementV.x < 0) desiredMovementV.x = 0;
        if(isRightLimit && desiredMovementV.x > 0) desiredMovementV.x = 0;
    }

    private bool IsInLimitPointByRay(Vector3 direction)
    {
        float radius = _circleLayout.GetRadius() == 0 ? 1f : _circleLayout.GetRadius();
        return Physics.Raycast(transform.position, direction, radius, _detectionMask);
    }

    private void OnCreateImpostors(GameObject go)
    {
        CharacterControllerIA character = go.GetComponent<CharacterControllerIA>();
        if (character.TryGetComponent(out CharacterAnimations chAnim))
        {
            chAnim.SetController(this);
        }
        character.OnDead += OnCharacterDead;
        _health = _multiplicable.GetCount();
    }
    private void OnDestroyImpostors(GameObject go)
    {
        CharacterControllerIA character = go.GetComponent<CharacterControllerIA>();
        if (character.TryGetComponent(out CharacterAnimations chAnim))
        {
            chAnim.RemoveController();
        }
        character.OnDead -= OnCharacterDead;
    }


    private void OnCreateImpostorsDone()
    {
        _circleLayout.Organize();
    }

    private void OnCharacterDead(LivingEntity entity)
    {
        _multiplicable.Remove(entity.gameObject);
        _currentTimeRepeatRateToOrganize = 0;
        _isOrganized = false;
        Damage(1);
    }

    public override void OnInitialize()
    {
        IInput input = new PlayerInput(_inputListener, this);
        _characterInput.Initialize(input);
        GenerateDefautPlayer();
    }

    public void GenerateDefautPlayer()
    {
        if (_multiplicable.GetCount() > 0)
        {
            _multiplicable.Remove(_multiplicable.GetCount());
        }

        _multiplicable.Generate(1, transform);
    }

    protected override void Dead()
    {
        base.Dead();
    }

    public override void OnTerminate()
    {
        _multiplicable.OnCreate -= OnCreateImpostors;
        _multiplicable.OnCreateDone -= OnCreateImpostorsDone;
        _multiplicable.OnDestroy -= OnDestroyImpostors;
    }

}
