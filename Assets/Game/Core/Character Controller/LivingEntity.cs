using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField]
    protected float _health;
    [SerializeField]
    protected float _maxHealth;

    public bool IsDead { get => _health <= 0; }

    private void Start()
    {
        _health = _maxHealth;
    }

    public void Damage(float damage)
    {
        _health -= damage;

        if (IsDead)
        {
            _health = 0;
            Dead();
        }

    }

    protected virtual void Dead() { }
    
}
