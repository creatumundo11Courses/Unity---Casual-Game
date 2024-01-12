using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerPlayer : CharacterControllerBase
{
    [SerializeField]
    private InputListenerSO _inputListener;
    [SerializeField]
    private Multiplicable _multiplicable;

    private void Awake()
    {
        _multiplicable.OnCreate += OnCreateImpostors;
        _multiplicable.OnDestroy += OnDestroyImpostors;
    }
    private void OnCreateImpostors(GameObject go)
    {
        CharacterControllerIA character = go.GetComponent<CharacterControllerIA>();
        character.OnDead += OnCharacterDead;
        _health = _multiplicable.GetCount();
    }
    private void OnDestroyImpostors(GameObject go)
    {
        CharacterControllerIA character = go.GetComponent<CharacterControllerIA>();
        character.OnDead -= OnCharacterDead;
    }

    private void OnCharacterDead(LivingEntity entity)
    {
        _multiplicable.Remove(entity.gameObject);
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
        Debug.Log("EL personaje principal ha muerto");
    }

}
