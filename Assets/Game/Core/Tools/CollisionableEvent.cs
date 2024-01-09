using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionableEvent : MonoBehaviour
{
    [SerializeField]
    private LayerMask _collisionMask;

    public UnityEvent<Collision> OnCollisionEnterEvent;
    public UnityEvent<Collider> OnTriggerEnterEvent;
    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & _collisionMask) != 0) // 0001 => 1000
        {
            OnCollisionEnterEvent?.Invoke(collision);
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _collisionMask) != 0)
        {
            OnTriggerEnterEvent?.Invoke(other);
        }
    }
}
