using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OperationType
{
    SUM,
    MULT,
    DIV,
    POW
}
[RequireComponent(typeof(CollisionableEvent))]
public class CollisionableMultiplier : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro _multiplierTMP;
    [SerializeField]
    private int _multiplier;
    [SerializeField]
    private OperationType _operationType = OperationType.SUM;
    private CollisionableEvent _collision;

    public event Action<CollisionableMultiplier, Collider> OnTriggerEnter;

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _collision = GetComponent<CollisionableEvent>();
        _collision.OnTriggerEnterEvent.AddListener(OnCollisionMade);
        _multiplierTMP.text = "";
    }
    public void SetMultiplier(int multiplier)
    {
        _multiplier = multiplier;
        _multiplierTMP.text = _multiplierTMP.text + multiplier;
    }
    public void SetOperationType(OperationType operationType)
    {
        _operationType = operationType;

        switch (_operationType)
        {
            case OperationType.SUM:
                _multiplierTMP.text = "+" + _multiplierTMP.text;
                break;
            case OperationType.MULT:
                _multiplierTMP.text = "X" + _multiplierTMP.text;
                break;
            case OperationType.DIV:
                _multiplierTMP.text = "/" + _multiplierTMP.text;
                break;
            case OperationType.POW:
                _multiplierTMP.text = "^" + _multiplierTMP.text;
                break;
        }

    }
    public int GetOperationResult(int currentValue)
    {
        switch (_operationType)
        {
            case OperationType.SUM:
                return _multiplier;
            case OperationType.MULT:
                return _multiplier * currentValue;
            case OperationType.DIV:
                return currentValue / _multiplier;
            case OperationType.POW:
                return (int)Mathf.Pow(currentValue, _multiplier);
        }

        return 0;
    }

    private void OnDestroy()
    {
        _collision.OnTriggerEnterEvent.RemoveAllListeners();
    }

    private void OnCollisionMade(Collider col)
    {
        OnTriggerEnter?.Invoke(this, col);
    }
}
