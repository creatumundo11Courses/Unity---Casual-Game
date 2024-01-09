using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControllerBase : LivingEntity
{
    [SerializeField]
    private CharacterMove _characterMove;
    [SerializeField]
    protected CharacterInput _characterInput;
    private Vector3 _desiredMovementV;


    private void Start()
    {
        OnInitialize();
    }

    private void Update()
    {
        OnUpdate();
    }

    private void OnDestroy()
    {
        OnTerminate();
    }
    public virtual void OnInitialize() { }
    public virtual void OnUpdate() 
    {
        _characterMove.Move(_desiredMovementV);
    }
    public virtual void OnTerminate() { }

    public void Move(Vector2 moveV)
    {
        _desiredMovementV = new Vector3(moveV.x, 0, moveV.y);
    }
    protected override void Dead()
    {
        
    }
}
