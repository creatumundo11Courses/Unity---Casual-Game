using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionableMultiplierContent : MonoBehaviour
{
    [SerializeField]
    private CollisionableMultiplier[] _collisionableMultipliers;
    public CollisionableMultiplier[] CollisionableMultipliers { get => _collisionableMultipliers; set => _collisionableMultipliers = value; }

    private bool _isColliding = false;

    

    private void Start()
    {
        foreach (var collisionable in _collisionableMultipliers)
        {
            collisionable.OnTriggerEnter += OnCollisionMade;
        }

        _isColliding = false;
    }

    private void OnDestroy()
    {
        foreach (var collisionable in _collisionableMultipliers)
        {
            collisionable.OnTriggerEnter -= OnCollisionMade;
        }
    }

    private void OnCollisionMade(CollisionableMultiplier collisionableMultiplier, Collider collider)
    {
        if (_isColliding) return;

        if (collider.TryGetComponent(out Multiplicable multiplicable))
        {
            _isColliding = true;
            int currentValue = multiplicable.GetCount();
            int numberInstantiation = collisionableMultiplier.GetOperationResult(currentValue);
            multiplicable.Generate(numberInstantiation, collider.transform);
        }
    }
    public void SetOperationToCollisionableMultiplier(int multiplier, OperationType operationType, int i)
    {
        _collisionableMultipliers[i].Initialize();
        _collisionableMultipliers[i].SetMultiplier(multiplier);
        _collisionableMultipliers[i].SetOperationType(operationType);

    }
}
