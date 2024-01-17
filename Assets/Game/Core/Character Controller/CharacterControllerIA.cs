using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerIA : CharacterControllerBase
{
    [HideInInspector]
    public Transform Target;
    [SerializeField]
    private float _damageValue;
    [SerializeField]
    private AudioClip _deadSFX;

    public override void OnInitialize()
    {
        IInput inputInstance = new IAInput(this);
        _characterInput.Initialize(inputInstance);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Collider[] colliders =  Physics.OverlapSphere(transform.position, _radius, _detectionMask);
        if (colliders.Length > 0)
        {
            Target = colliders[0].transform;
        }
    }

    public void AddDamage(Collider col)
    {
        if (col.TryGetComponent(out LivingEntity damageable))
        {
            damageable.Damage(_damageValue);
        }
    }

    protected override void Dead()
    {
        base.Dead();
        gameObject.SetActive(false);
        GameAudio.PlayEffectAudio(_deadSFX,0.2f);
        Destroy(gameObject);
    }

   
}
